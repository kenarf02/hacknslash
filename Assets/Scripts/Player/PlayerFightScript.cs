using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;
public class PlayerFightScript : MonoBehaviour
{
    public int HP;
    public int MaxHP;

    public int Mana;
    public int MaxMana;

    public int Armor;
    public int STR, DEX, INT;

    public int MaxDmg;
    public int MinDmg;

    public float attackspeed;
    public float attackRange;

    public bool spellbeingcast;

    [Tooltip("Prefab of popping up number")]
    public GameObject PopUpPrefab;
    public GameObject SpellCircle;
    GameObject Circle;

    public GameObject target;
    float timer;
    float castTimer;
    float manaregentimer=10f;

    [SerializeField]
    EquipmentManager equipmentManager;
    SpellBook spellBook;
    GameManager gm;
    PlayerController playerController;
    NavMeshAgent agent;
    PlayerMotor motor;
    Animator anim;
    UIControl ui;
    [SerializeField]
    LevelObject levelObject;
    #region delegates for spells

   public Spell[] spells = new Spell[] { null, null, null };
    public float[] cooldowns = new float[] {0,0,0 };

    [Tooltip("Spell to be cast")]
    public Spell chosenSpell;

    #endregion
    #region equipment
    public GameObject helditem;
    public GameObject Helmet;
    public GameObject Body;
    public GameObject Trousers;
    public GameObject Boots;
    #endregion
    private void Start()
    {
        InitializeFromLevelObject();
        timer = 0;
        equipmentManager = GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>();
        UpdateStats();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        spellBook = GameObject.Find("GAME MANAGER").GetComponent<SpellBook>();
        playerController = GetComponent<PlayerController>();
        gm = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        motor = GetComponent<PlayerMotor>();
        ui = gm.GetComponent<UIControl>();
    }
    private void Update()
    {
        ManaTimer();
        ListenForSpellCasts();
        ReduceCoolDowns();
        if (target != null)
        {
            if (!playerController.isCasting)
            {
                if ((target.transform.position - transform.position).magnitude <= attackRange)
                {
                    Attack();
                    anim.SetBool("Run", false);
                    agent.SetDestination(transform.position);
                }
                else
                {
                    motor.MoveToPoint(target.transform.position);
                }
            }
            else
            {
                if ((target.transform.position - transform.position).magnitude <= chosenSpell.range || chosenSpell.range == 0f)
                {
                    Attack();
                    anim.SetBool("Run", false);
                }
                else
                {
                    motor.MoveToPoint(target.transform.position);
                }
            }

        }
        else if (chosenSpell != null)
        {
           if (chosenSpell.range == 0f)
            {
                CastSpell(gameObject);
                target = null;
            }
        }
    
    }
    public void Attack()
    {
        if (!playerController.isCasting)
        {
            anim.SetBool("Run", false);
            if (target.GetComponent<EnemyScript>())
            {
                if (timer <= 0)
                {
                    transform.LookAt(target.transform);
                    anim.SetBool("Run", false);
                    anim.SetTrigger("Hit");
                    timer = attackspeed;
                    target.GetComponent<EnemyScript>().GetDamage(RollDmg());
                    target.GetComponent<EnemyScript>().lastDmgType = LastDmg.Melee;
                }
                else
                {
                    timer -= Time.deltaTime;
                    anim.SetBool("Run", false);
                }
            }
        }
        else
        {
            CastSpell(target);
        }
    }
    public void GetDamage(int Damage)
    {
        if (Damage - Armor >= 0)
        {
            HP -= (Damage - Armor);
            GameObject param = Instantiate(PopUpPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
            param.transform.GetChild(0).GetComponent<TextMesh>().text = (Damage - Armor).ToString();
        }
        else
        {
            HP -= 0;
            GameObject param = Instantiate(PopUpPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
            param.transform.GetChild(0).GetComponent<TextMesh>().text = "0";
        }
        gm.GetComponent<UIControl>().updateHealth(HP);

    }
    public void Heal(int amount)
    {
       
        if (HP + amount <= MaxHP)
        {
            HP += amount;
            GameObject param = Instantiate(PopUpPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
            param.transform.GetChild(0).GetComponent<TextMesh>().text = amount.ToString();
            param.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(0.1921569f, 0.7960784f, 0f);
        }
        else
        {
            
            GameObject param = Instantiate(PopUpPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
            if (HP != MaxHP)
            {
                param.transform.GetChild(0).GetComponent<TextMesh>().text = (Mathf.Abs(amount - HP)).ToString();
            }
            else
            {
                param.transform.GetChild(0).GetComponent<TextMesh>().text = "0";
            }
            HP = MaxHP;
            param.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(0.1921569f, 0.7960784f, 0f);
        }
        gm.GetComponent<UIControl>().updateHealth(HP);
    }
    public void ManaTimer()
    {
        if (manaregentimer <= 0)
        {
            RegenMana(1);
            manaregentimer = 10f;
        }
        else
        {
            manaregentimer -= Time.deltaTime;
        }
    }
    public void RegenMana(int amount)
    {
        if (Mana + amount <= MaxMana)
        {
            Mana += amount;
            GameObject param = Instantiate(PopUpPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.identity);
            param.transform.GetChild(0).GetComponent<TextMesh>().text = amount.ToString();
            param.transform.GetChild(0).GetComponent<TextMesh>().color = new Color32(81,187,254,255);
        }
        ui.updateMana(Mana);
        ui.CheckIfCanAffordSpells(Mana, manacosts());
    }
    int RollDmg()
    {
        return UnityEngine.Random.Range(MinDmg, MaxDmg);
    }
    public int[] manacosts()
    {
        int[] param = new int[3];
        for (int i = 0; i < spells.Length; i++)
        {
            if (spells[i] != null)
            {
                param[i] = spells[i].manacost;
            }
            else
            {
                param[i] = 0;
            }
        }
        return param;
    }
    void CastSpell(GameObject spelltrgt)
    {
        Debug.Log("Spell cast: " + chosenSpell.GetType().Name);
        //TODO: ADD ANIMATIONS!

        if (spellbeingcast == false)
        {
            Mana -= chosenSpell.manacost;
            ui.updateMana(Mana);
            ui.CheckIfCanAffordSpells(Mana, manacosts());
            spellbeingcast = true;
            transform.LookAt(spelltrgt.transform.position);
            castTimer = chosenSpell.castTime;
            motor.MoveToPoint(transform.position);
            gm.PlayerCanWalk = false;
            DeleteCircle();
        }
        if (castTimer >= 0)
        {
            castTimer -= Time.deltaTime;
        }
        else
        {
            if (target != null)
            {
                target.GetComponent<EnemyScript>().lastDmgType = LastDmg.Spell;
            }
            cooldowns[Array.IndexOf(spells, chosenSpell)] = chosenSpell.cooldown;
            anim.SetTrigger("Hit");
            chosenSpell.OnUse(spelltrgt, RollDmg());
            playerController.isCasting = false;
            chosenSpell = null;
            gm.PlayerCanWalk = true ;
            target = null;
            spellbeingcast = false;
        }
    }
    void SelectSpell(Spell sp)
    {
            CreateCircle(sp);
            playerController.isCasting = true;
            chosenSpell = sp;
            Debug.Log("Spell chosen: " + chosenSpell.GetType().Name);
    }
    void CreateCircle(Spell sp)
    {
        if (Circle == null)
        {
            Circle = Instantiate(SpellCircle, transform.position + new Vector3(0, -0.97f, 0), Quaternion.identity, transform);
            Circle.GetComponent<CircleIndicator>().radius = sp.range;
            Circle.GetComponent<CircleIndicator>().DoRenderer();
        }
    }
    public void DeleteCircle()
    {
        if (Circle != null)
        {
            Destroy(Circle);
        }

    }
    public void UpdateStats() 
    {
      List<Item> param = equipmentManager.EquippedItems();
        int bonusSTR = 0;
        int bonusINT = 0;
        int bonusDEX = 0;
        int bonusArmor = 0;
        foreach (Item item in param)
        {
            if (item != null)
            {
                if (item.stats.ContainsKey("MaxDMG"))
                {
                    MaxDmg = item.stats["MaxDMG"];
                }
                if (item.stats.ContainsKey("MinDMG"))
                {
                    MinDmg = item.stats["MinDMG"];
                }
                if (item.stats.ContainsKey("AttackSpeed"))
                {
                    attackspeed = item.stats["AttackSpeed"];
                }
                if (item.stats.ContainsKey("AttackRange"))
                {
                    attackspeed = item.stats["AttackRange"];
                }
                if (item.stats.ContainsKey("BonusSTR"))
                {
                    bonusSTR += item.stats["BonusSTR"];
                    Debug.Log(item.stats["BonusSTR"]);
                }
                if (item.stats.ContainsKey("BonusDEX"))
                {
                    bonusDEX += item.stats["BonusDEX"];
                    Debug.Log(item.stats["BonusDEX"]);
                }
                if (item.stats.ContainsKey("BonusINT"))
                {
                    bonusINT += item.stats["BonusINT"];
                    Debug.Log(item.stats["BonusINT"]);
                }
                if (item.stats.ContainsKey("Armor"))
                {
                    bonusArmor += item.stats["Armor"];
                }
            }
            }
        STR = bonusSTR;
        INT = bonusINT;
        DEX = bonusDEX;
        Armor = bonusArmor;
    }
    void ListenForSpellCasts()
    {
        if (Input.GetButtonDown("SpellOne"))
        {
            if (spells[0] != null)
            {
                if (cooldowns[0] <= 0)
                {
                    if (spells[0].manacost <= Mana)
                    {
                        SelectSpell(spells[0]);
                    }
                }
            }

        }
        if (Input.GetButtonDown("SpellTwo"))
        {
            if (spells[1] != null)
            {
                if (cooldowns[1] <= 0)
                {
                    if (spells[1].manacost <= Mana)
                    {
                        SelectSpell(spells[1]);
                    }
                }
            }
        }
        if (Input.GetButtonDown("SpellThree"))
        {
            if (spells[2] != null)
            {
                if (cooldowns[2] <= 0)
                {
                    if (spells[2].manacost <= Mana)
                    {
                        SelectSpell(spells[2]);
                    }
                }
            }
        }

    }
    void ReduceCoolDowns()
    {
        for (int i = 0; i < cooldowns.Length; i++)
        {
            if (cooldowns[i] > 0)
            {
                cooldowns[i] -= Time.deltaTime;
                ui.UpdateSpellsCoolDowns(cooldowns);
                ui.CheckIfCanAffordSpells(Mana, manacosts());
            }
        }
    }
    void InitializeFromLevelObject()
    {
        levelObject.Load();
        HP = levelObject.Health;
        MaxHP = levelObject.MaxHealth;
        Mana = levelObject.Mana;
        MaxMana = levelObject.MaxMana;
    }
}


using UnityEngine;
using UnityEngine.AI;

public enum LastDmg
{
    Melee, Spell   
}
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    EnemyUIScript ui;
    GameManager gm;

    [Tooltip("Range of sight..describes from how far the enemy sees the player")]
    public float rangeofSight;
    [Tooltip("Attack Range")]
    public float AttackRange;

    public int HP;
    public int Armor;

    public float AttackSpeed;
    public float damagedelay;
    public int maxDamage;
    public int minDamage;

    public LastDmg lastDmgType;

    private float timer;

    bool following;
    bool damaged;

    public GameObject DropPrefab;
    [Tooltip("Prefab of popup number damage indicator")]
    public GameObject PopUpPrefab;

    [Tooltip("IDs of items that can drop from this mob")]
    public int[] DropItems;
    [Tooltip("Chances of dropping 0-100, rates must be in the same index as the items")]
    public int[] DropRate;

    private Animator anim;
    void Start()
    {
        gm = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        timer = 0;
        ui = GetComponent<EnemyUIScript>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Seek();
        if (HP <= 0)
        {
            Die();
        }
    }

    void Seek()
    {
        if ((player.transform.position - transform.position).magnitude <= rangeofSight || following == true)
        {
            following = true;
            Follow();
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }
    void Follow()
    {
        if ((player.transform.position - transform.position).magnitude >= rangeofSight && damaged == false)
        {
            following = false;
        }
        if (following)
        {
            agent.SetDestination(player.transform.position);
            anim.SetBool("Walk", true);
            if ((player.transform.position - transform.position).magnitude <= AttackRange)
            {
                Attack();
                anim.SetBool("Walk", false);
                agent.SetDestination(transform.position);
            }
        }
    }
    bool played = false;
    void Attack()
    {

        if (timer <= 0)
        {
            played = false;
            if (played == false)
            {

                Vector3 targetPostition = new Vector3(player.transform.position.x,
                this.transform.position.y,
                player.transform.position.z);
                transform.LookAt(targetPostition);
                anim.SetTrigger("Attack");
                Debug.Log("Attacked");
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= damagedelay)
            {
                played = true;
                anim.ResetTrigger("Attack");
                timer = AttackSpeed;
                Debug.Log("Attacked");
                player.GetComponent<PlayerFightScript>().GetDamage(RollDamage());
            }
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    float GetAttackAnimLength()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Attack")
            {
                return clip.length;
            }
        }
        Debug.LogError("No such clip");
        return -1;
    }
    int RollDamage()
    {
        return Random.Range(minDamage, maxDamage);
    }
    void Die()
    {
        DropItem();
        AddExp();
        SetPlayerToNotFighting();
        Destroy(this.gameObject);
    }
    void SetPlayerToNotFighting()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.isCasting = false;
        player.GetComponent<PlayerFightScript>().chosenSpell = null;
        gm.PlayerCanWalk = true;
        player.GetComponent<PlayerFightScript>().spellbeingcast = false;
    }
    void DropItem()
    {
        int x = Random.Range(0, 100);
        for (int i = 0; i < DropItems.Length; i++)
        {
            if (DropRate[i] >= x)
            {
                GameObject param = Instantiate(DropPrefab, transform.position, Quaternion.identity);
                param.GetComponent<DropItemScript>().item = GameObject.Find("EQUIPMENT MANAGER").GetComponent<ItemDatabase>().Items[DropItems[i]];
            }
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
        damaged = true;
        following = true;
        Invoke("TurnDamagedOff", 3f);
        ui.UpdateHealthBar(HP);
    }


    void TurnDamagedOff()
    {
        damaged = false;
    }

   
    void AddExp() 
        {
        if (lastDmgType == LastDmg.Melee) {
            Debug.Log("Added exp");
            gm.GetComponent<Exp>().expCategories[0].AddExp(10);

        }
        else if (lastDmgType == LastDmg.Spell)
        {
            gm.GetComponent<Exp>().expCategories[1].AddExp(10);
        }
        gm.GetComponent<Exp>().CheckforLevelUp();
    }
    
}

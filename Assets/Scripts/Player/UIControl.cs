using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIControl : MonoBehaviour
{
    EquipmentManager equipmentManager;
    [SerializeField]
    GameObject SpellUI;
    GameManager gm;
    [SerializeField]
    Slider HealthBar;
    [SerializeField]
    Slider ManaBar;
    [SerializeField]
    Text levelText;
    [SerializeField][Tooltip("Slots in spell InGame UI")]
    GameObject[] spellslots;
    Image[] spellslotsicons = new Image[3];
    [SerializeField]
    LevelObject levelObject;
    private void Start()
    {
        equipmentManager = GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>();
        gm = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        UpdateSpells(SpellUI.GetComponent<SpellBookUI>().chosenspells);
        levelObject.Load();
        SetLevel(levelObject.Playerlevel);
        SetMaxValueHP(levelObject.MaxHealth);
        SetMaxValueMana(levelObject.MaxMana);
        updateHealth(levelObject.Health);
        updateMana(levelObject.Mana);
        for(int i = 0; i < spellslots.Length; i++)
        {
            spellslotsicons[i] = spellslots[i].transform.Find("Icon").GetComponent<Image>();
            spellslotsicons[i].type = Image.Type.Filled;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            equipmentManager.OpenUI();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EnableSpellUI();
        }
    }

    void EnableSpellUI()
    {
        if (!SpellUI.activeInHierarchy)
        {
            SpellUI.SetActive(true);
            gm.PlayerCanWalk = false;
        }
        else
        {
            SpellUI.SetActive(false);
            gm.PlayerCanWalk = true;

        }
    }
    public void updateHealth(int currentamount)
    {
        HealthBar.value = currentamount;
        levelObject.Health = currentamount;
    }
    public void updateMana(int currentamount)
    {
        ManaBar.value = currentamount;
        levelObject.Mana = currentamount;
    }
    public void SetMaxValueHP(int amount)
    {
        HealthBar.maxValue = amount;
    }
    public void SetMaxValueMana(int amount)
    {
        ManaBar.maxValue = amount;
    }
    public void SetLevel(int level)
    {
        levelText.text = "Level: "+level.ToString();
        levelObject.Playerlevel = level;
    }
    public void UpdateSpells(int[] spells)
    {
        int index = 0;
        foreach (GameObject spellslot in spellslots)
        {
            GameObject param = spellslot.transform.Find("Icon").gameObject;
            if (spells[index] != -1)
            {
                param.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                param.GetComponent<Image>().sprite = GetComponent<SpellBook>().SpellDataBase[spells[index]].icon;
            }
            else
            {
                param.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            }
            index++;
        }
    }
    public void UpdateSpellsCoolDowns(float[] cooldowns)
    {
        int index = 0;
        foreach(Image i in spellslotsicons)
        {
            if (cooldowns[index] > 0)
            {
                i.fillAmount = 1 - cooldowns[index];
            }
            else
            {
                i.fillAmount = 1;
            }
            index++;
        }
    }
    public void CheckIfCanAffordSpells(int mana, int[] costs)
    {
        for (int i = 0; i < costs.Length; i++)
        {
            if (mana >= costs[i])
            {
                if (spellslotsicons[i].sprite != null)
                {
                    spellslotsicons[i].color = Color.white;
                }
            }
            else
            {
                if (spellslotsicons[i].sprite != null)
                {
                    spellslotsicons[i].color = Color.red;
                }
            }
        }
    }
}

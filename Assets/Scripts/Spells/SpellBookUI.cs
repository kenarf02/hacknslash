using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SpellBookUI : MonoBehaviour
{
    [SerializeField]
    GameObject SpellSlotPrefab;
    SpellBook spellBook;
    EquipmentManager equipmentManager;
    PlayerFightScript playerFightScript;
    [SerializeField]
    GameObject AvaliableSpells;
    [SerializeField]
    GameObject ActiveSpells;
    [SerializeField]
    public int[] chosenspells;
    [SerializeField]
    UIControl uIControl;

    private void Awake()
    {
        Initialize();
    }
    public void Initialize()
    {
        playerFightScript = GameObject.Find("Player").GetComponent<PlayerFightScript>();
        chosenspells = new int[] { -1, -1, -1 };
        spellBook = GameObject.Find("GAME MANAGER").GetComponent<SpellBook>();
        equipmentManager = GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>();
        loadPlayerPrefsSpells();
        uIControl.CheckIfCanAffordSpells(playerFightScript.Mana,playerFightScript.manacosts());
    }
    private void OnEnable()
    {
        InitializeSpells();
        OnChosenSpellsChange();
    }
    void InitializeSpells()
    {
        for(int i = 0; i <ActiveSpells.transform.childCount;i++)
        {
            if (chosenspells[i] != -1) {
                Debug.Log("Changed");
                ActiveSpells.transform.GetChild(i).transform.Find("Icon").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                ActiveSpells.transform.GetChild(i).transform.Find("Icon").GetComponent<Image>().sprite = spellBook.SpellDataBase[chosenspells[i]].icon;
            }
            else
            {
                ActiveSpells.transform.GetChild(i).transform.Find("Icon").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            }
        }
        foreach (Transform child in AvaliableSpells.transform)
        {
            Destroy(child.gameObject);
        }
        foreach(Spell spell in spellBook.SpellDataBase)
        {
           GameObject param= Instantiate(SpellSlotPrefab, AvaliableSpells.transform);
            param.transform.Find("Icon").GetComponent<Image>().sprite = spell.icon;
            param.GetComponent<SpellSlotBehavior>().spell = spellBook.SpellDataBase.IndexOf(spell);
        }
        UpdateInGameUI();
    }
    void OnChosenSpellsChange()
    {
        for (int i = 0; i < chosenspells.Length; i++)
        {
            PlayerPrefs.SetInt("Spell" + i.ToString(), chosenspells[i]);
            if (chosenspells[i] != -1)
            {
                playerFightScript.spells[i] = spellBook.SpellDataBase[chosenspells[i]];
                playerFightScript.cooldowns[i] = spellBook.SpellDataBase[chosenspells[i]].cooldown;
            }
            else
            {
                playerFightScript.spells[i] = null;
            }
            UpdateInGameUI();
        }
    }
    public void AddSpell(int spellid)
    {
        // Check wheter any index is -1 (no spell)
        for(int i = 0; i < chosenspells.Length; i++)
        {
            if(chosenspells[i] == -1)
            {
                if (!chosenspells.ToList<int>().Contains(spellid)) {
                    chosenspells[i] = spellid;
                    OnChosenSpellsChange();
                    Debug.LogError("New spell set");
                    InitializeSpells();
                    return;
                }
            }
        }
        //TODO: PRINT SOME NOTIFICATION SAYING THERE IS NO FREE SPACE FOR OUR SPELL
    }

    private void OnDisable()
    {
        OnChosenSpellsChange();
    }
    public void DeleteSpellSlot(int index)
    {
        chosenspells[index] = -1;
        InitializeSpells();
        OnChosenSpellsChange();
        UpdateInGameUI();
    }
    public void Replace(string indexes)
    {
       string[] splittedindexes = indexes.Split(',');
        int param = chosenspells[int.Parse(splittedindexes[0])];
        chosenspells[int.Parse(splittedindexes[0])] = chosenspells[int.Parse(splittedindexes[1])];
        chosenspells[int.Parse(splittedindexes[1])] = param;
        InitializeSpells();
        OnChosenSpellsChange();
        UpdateInGameUI();
    }
    void UpdateInGameUI()
    {
        uIControl.UpdateSpells(chosenspells);
    }
    void loadPlayerPrefsSpells()
    {
        //Initialize spells from playerprefs
        for (int i = 0; i < chosenspells.Length; i++)
        {
            if (PlayerPrefs.HasKey("Spell" + i.ToString()))
            {
                chosenspells[i] = PlayerPrefs.GetInt("Spell" + i.ToString());
            }
            else
            {
                chosenspells = new int[] { -1, -1, -1 };
            }
        }
        OnChosenSpellsChange();
        UpdateInGameUI();
    }
}

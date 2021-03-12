using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool PlayerCanWalk;
    public int Money;
    public int Level;
    [SerializeField]
    GameObject player;
    [SerializeField]
    LevelObject levelobj;
    [SerializeField]
    EqSaver eqSaver;
    [SerializeField]
    SpellBookUI spellBookUI;
    private void Awake()
    {
        GetComponent<SpellBook>().buildDataBase();
        levelobj.Load();
        if (levelobj.isinside)
        {
            if(SceneManager.GetActiveScene().name == "Overworld")
            {
                SceneManager.LoadScene(1);
            }
            GameObject.Find("INSIDEMANAGER").GetComponent<InsideManager>().Spawn();
        }
        else
        {
            if (SceneManager.GetActiveScene().name != "Overworld")
            {
                SceneManager.LoadScene(0);
            }
        }
        spellBookUI.Initialize();
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            player.transform.position = levelobj.OverworldPosition;
        }
        
        if (PlayerPrefs.HasKey("LevelInitialized"))
        {
            if (PlayerPrefs.GetInt("LevelInitialized") == 0)
            {
                levelobj.SetDefaultPlayerPrefs();
            }
        }
        else
        {
            levelobj.SetDefaultPlayerPrefs();
        }
        if (PlayerPrefs.HasKey("EqInitialized"))
        {
            if (PlayerPrefs.GetInt("EqInitialized") == 0)
            {
                eqSaver.SetDefaultPlayerPrefs();
            }
            else
            {
                eqSaver.Load();
            }
        }
        else
        {
            eqSaver.SetDefaultPlayerPrefs();
        }
        GameObject.Find("EQUIPMENT MANAGER").GetComponent<ItemDatabase>().BuildDatabase();
        GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>().LoadFromEqSaver();

        Debug.Log("Initialized");
        Debug.Log(PlayerPrefs.GetFloat("ZPOS"));
    }
    private void Start()
    {
    }
    public void Save()
    {
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            levelobj.OverworldPosition = GameObject.Find("Player").transform.position;
        }
      
        levelobj.Playerlevel = Level;
        levelobj.Save();
        GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>().SaveEq();
        GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>().Save();
    }
    public void Savestats()
    {
        PlayerFightScript playerFightScript = player.GetComponent<PlayerFightScript>();
        levelobj.Money = Money;
        levelobj.Mana = playerFightScript.Mana;
        levelobj.MaxMana = playerFightScript.MaxMana;
        levelobj.MaxHealth = playerFightScript.MaxHP;
        levelobj.Health = playerFightScript.HP;
        levelobj.Save();
        GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>().SaveEq();
        GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>().Save();
    }
    private void OnApplicationQuit()
    {
        Save();
        Savestats();
        

    }

}

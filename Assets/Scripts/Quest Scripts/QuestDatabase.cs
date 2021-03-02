using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public Quest[] Quests;

    private void Awake()
    {
        Quests = new Quest[] {
            new Quest(0, "A friend who is a frog", "Help Gabriela find her froggy friend", 7, 100)
            };
        
        Load();
    }

    public void Save()
    {
        foreach(Quest quest in Quests)
        {
            PlayerPrefs.SetInt(quest.id.ToString() + "Complete",quest.Completed?1:0);
            PlayerPrefs.SetInt(quest.id.ToString() + "Active",quest.isActive?1:0);
        }
        Debug.Log("Quests saved");
    }
    public void Load()
    {
        foreach(Quest quest in Quests)
        {
            quest.Completed = PlayerPrefs.GetInt(quest.id.ToString() + "Complete") == 1 ? true : false;
            quest.isActive = PlayerPrefs.GetInt(quest.id.ToString() + "Active") == 1 ? true : false;
        }
    }
}

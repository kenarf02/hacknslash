using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public Quest[] Quests;

    private void Awake()
    {
        Quests = new Quest[] {
            new Quest(0, "A friend who is a frog", "Help Gabriela find her froggy friend", 7, 50,0),
            new Quest(1, "Rat hunt", "Help Theodor to kill 6 rats",-1,50,7)
            }
        ;
        
        Load();
    }

    public void Save()
    {
        foreach(Quest quest in Quests)
        {
            PlayerPrefs.SetInt(quest.id.ToString() + "Complete",quest.Completed?1:0);
            PlayerPrefs.SetInt(quest.id.ToString() + "Active",quest.isActive?1:0);
            PlayerPrefs.SetInt(quest.id.ToString() + "Progress", quest.progress);
        }
        Debug.Log("Quests saved");
    }
    public void Load()
    {
        foreach(Quest quest in Quests)
        {
            quest.Completed = PlayerPrefs.GetInt(quest.id.ToString() + "Complete") == 1 ? true : false;
            quest.isActive = PlayerPrefs.GetInt(quest.id.ToString() + "Active") == 1 ? true : false;
            if (PlayerPrefs.HasKey(quest.id.ToString()))
            {
                if (PlayerPrefs.GetInt(quest.id.ToString() + "Progress")!= 0 )
                {
                    if (!quest.Completed)
                    {
                        quest.progress = PlayerPrefs.GetInt(quest.id.ToString() + "Progress");
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDatabase : MonoBehaviour
{
    public List<Quest> Quests;

    private void Awake()
    {
        Quests = new List<Quest> {
            new Quest("A friend who is a frog","Help Gabriela find her froggy friend",7,100)
       
        };
    }
}

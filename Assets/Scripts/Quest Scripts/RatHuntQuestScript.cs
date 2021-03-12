using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHuntQuestScript : MonoBehaviour
{
    QuestDatabase quests;
    [SerializeField]
    int OnGoingQuest;
    private void Start()
    {
        quests = GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>();
    }
    private void OnDestroy()
    {
        if (quests.Quests[OnGoingQuest].isActive && !quests.Quests[OnGoingQuest].Completed)
        {
            Debug.Log(quests.Quests[OnGoingQuest].title + "updated");
            quests.Quests[OnGoingQuest].ChangeProgress(1);
        }
    }
}

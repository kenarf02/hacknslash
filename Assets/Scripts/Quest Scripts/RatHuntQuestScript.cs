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
        Debug.Log("RatQuest updated");
        quests.Quests[OnGoingQuest].ChangeProgress(1);
    }
}

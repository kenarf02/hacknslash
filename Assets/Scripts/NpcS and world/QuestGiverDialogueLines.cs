using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverDialogueLines : MonoBehaviour
{
    [SerializeField]
    [Tooltip("When the requirements are not met")]
    public DialogueFragment ongoingQuest;
    [SerializeField]
    [Tooltip("First dialogue after we finished the goal")]
    public DialogueFragment QuestDone;
    [SerializeField]
    [Tooltip("After we finished quest")]
    public DialogueFragment finishedQuest;
}

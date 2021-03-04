using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStartDialogue : MonoBehaviour
{
    public DialogueFragment thisNpcDialogue;
    public NpcDialogueScript npcDialogueScript;
    QuestDatabase quests;
    [SerializeField]
    public int givenquest =-1; // Quest being given by this npc
    private void Start()
    {
        npcDialogueScript = GameObject.Find("DIALOGUE MANAGER").GetComponent<NpcDialogueScript>();
        if (givenquest != -1) {
            quests = GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>();
            InitializeDialogueLine();
         }
    }
    public void StartDialogue()
    {
        if (npcDialogueScript.DialogueUI.activeInHierarchy != true)
        {
            npcDialogueScript.Initialize(thisNpcDialogue,gameObject);
        }
    }
    //Checks which dialogue line to give depending on quest situation
   public void InitializeDialogueLine()
    {
        if(!quests.Quests[givenquest].Completed && !quests.Quests[givenquest].isActive)
        {
        }
        else if(!quests.Quests[givenquest].Completed && quests.Quests[givenquest].isActive)
        {
            thisNpcDialogue = GetComponent<QuestGiverDialogueLines>().ongoingQuest;
        }
        else if(quests.Quests[givenquest].Completed && quests.Quests[givenquest].isActive)
        {
            thisNpcDialogue = GetComponent<QuestGiverDialogueLines>().QuestDone;
        }
        else
        {
            thisNpcDialogue = GetComponent<QuestGiverDialogueLines>().finishedQuest;
        }
    }


}

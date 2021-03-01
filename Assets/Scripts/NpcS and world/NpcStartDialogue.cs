using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStartDialogue : MonoBehaviour
{
    public DialogueFragment thisNpcDialogue;
    NpcDialogueScript npcDialogueScript;

    private void Start()
    {
        npcDialogueScript = GameObject.Find("DIALOGUE MANAGER").GetComponent<NpcDialogueScript>();
    }
    public void StartDialogue()
    {
        if (npcDialogueScript.DialogueUI.activeInHierarchy != true)
        {
            npcDialogueScript.Initialize(thisNpcDialogue,gameObject);
        }
    }



}

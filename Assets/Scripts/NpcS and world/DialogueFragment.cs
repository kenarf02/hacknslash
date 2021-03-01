using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueFragment")]
public class DialogueFragment: ScriptableObject
{
    public string Text;
    [Tooltip("String of possible anwsers")]
    public List<string> ChoiceKeys = new List<string>();
    [Tooltip("Dialogue fragments connected with choiceKeys list")]
    public List<DialogueFragment> ChoiceValues = new List<DialogueFragment>();
    public bool OpenShop;
    public bool StartsQuest;

    public int QuestId;
}

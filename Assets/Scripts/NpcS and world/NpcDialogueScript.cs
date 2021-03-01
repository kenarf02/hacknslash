using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DialogueFragment))]
public class NpcDialogueScript : MonoBehaviour
{
    public DialogueFragment fragment;
    public GameObject DialogueUI;
    public Text dialogueText;
    [Tooltip("Place to print all possible anwser options for player")]
    public GameObject optionsField;
    GameObject source;
    bool isactive = false;

    const float TEXTPRINTSPEED = 0.02f;

    [Tooltip("Prefab of button of dialogue choice")]
    public GameObject buttonOfOption;
    GameManager gameManager;
    ItemDatabase itemDatabase;

    //Options and such
    public GameObject Dialogue;
    //Shop
    public GameObject Shop;
    [SerializeField]
    GameObject Shopslot;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            EndDialogue();
        }
    }
    private void Start()
    {
        itemDatabase = GameObject.Find("EQUIPMENT MANAGER").GetComponent<ItemDatabase>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
    }
    public void Initialize(DialogueFragment _fragment, GameObject _src)
    {
        source = _src;
        dialogueText.gameObject.SetActive(true);
        Dialogue.SetActive(true);
        Shop.SetActive(false);
        if (!isactive)
        {
            //turn off the equipment ui
            GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>().EqActive = false;
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("UI"))
            {
                //Disable all other ui
                go.SetActive(false);
            }
            //Initialize chosen fragment
            fragment = _fragment;
            //Make sure player won't move
            gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
            gameManager.PlayerCanWalk = false;
            //open dialogueUI
            DialogueUI.SetActive(true);
            //Clean after last dialogue
            dialogueText.text = "";
            DestroyAllOptions();
            //Start printing text
            PrintText();
            if (fragment.StartsQuest)
            {
                StartQuest();
            }
        }
    }
    void DestroyAllOptions()
    {
        foreach(Transform child in optionsField.transform)
        {
            Destroy(child.gameObject);
        }
    }
    void PrintText()
    {
        if (!isactive)
        {
            StartCoroutine(displayText());
        }
    }
    void PrintAnswers()
    {
        if (fragment.ChoiceValues.Count != 0)
        {
            int index = 0;
            foreach (DialogueFragment frag in fragment.ChoiceValues)
            {
                GameObject param = Instantiate(buttonOfOption, optionsField.transform);
                param.transform.GetComponentInChildren<Text>().text = fragment.ChoiceKeys[index];
                param.name = index.ToString();
                param.GetComponent<Button>().onClick.AddListener(delegate { choice(Int32.Parse(param.name)); });
                index++;
            }
        }
        else
        {
            GameObject param = Instantiate(buttonOfOption, optionsField.transform);
            param.transform.GetComponentInChildren<Text>().text = "End conversation";
            param.GetComponent<Button>().onClick.AddListener(delegate { EndDialogue(); });
        }
    }

    public IEnumerator displayText()
    {
        isactive = true;
        foreach(char letter in fragment.Text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(TEXTPRINTSPEED);
        }
        PrintAnswers();
        isactive = false;
        StopAllCoroutines();
    }

    public void choice(int index)
    { 
        fragment = fragment.ChoiceValues[index];
        if (!fragment.OpenShop)
        {
            Initialize(fragment,source);
        }
        else
        {
            OpenShop();
        }
       
    }
    public void EndDialogue()
    {
        DialogueUI.SetActive(false);
        gameManager.PlayerCanWalk = true;
    }
    void StartQuest()
    {
        QuestDatabase qm = GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>();
        if (fragment.StartsQuest)
        {
            if (qm.Quests[fragment.QuestId].isActive != true && qm.Quests[fragment.QuestId].Completed != true)
            {
                qm.Quests[fragment.QuestId].isActive = true;
                Debug.Log("Quest started");
                source.GetComponent<NpcStartDialogue>().thisNpcDialogue = source.GetComponent<QuestGiverDialogueLines>().ongoingQuest;
            }
            else if(qm.Quests[fragment.QuestId].isActive && qm.Quests[fragment.QuestId].Completed == true)
            {
                Debug.Log("Given award for: "+ qm.Quests[fragment.QuestId].title);
                if (qm.Quests[fragment.QuestId].ItemReward != -1) {
                    GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>().AddItem(GameObject.Find("EQUIPMENT MANAGER").GetComponent<ItemDatabase>().Items[qm.Quests[fragment.QuestId].ItemReward]);     
                }
                if (qm.Quests[fragment.QuestId].GoldReward > 0)
                {
                    gameManager.Money += (qm.Quests[fragment.QuestId].GoldReward);
                }
                qm.Quests[fragment.QuestId].isActive = false;
                qm.Quests[fragment.QuestId].Completed = true;
                source.GetComponent<NpcStartDialogue>().thisNpcDialogue = source.GetComponent<QuestGiverDialogueLines>().finishedQuest;
            }
        }
        //TODO: Alternative dialogue if not completed but active
    }
    void OpenShop()
    {
        Dialogue.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        Shop.SetActive(true);
        if (source.GetComponent<ShopScript>()!= null)
        {
            int index = 0;
            foreach(int x in source.GetComponent<ShopScript>().items)
            {
                if (x != -1)
                {
                    GameObject param = Instantiate(Shopslot, Shop.transform.GetChild(0));
                    param.GetComponent<ShopSlotScript>().cost = source.GetComponent<ShopScript>().costs[index];
                    param.GetComponent<ShopSlotScript>().item = itemDatabase.Items[x];
                    param.GetComponent<ShopSlotScript>().InitializeIcon();
                    param.GetComponent<ShopSlotScript>().source = source;
                }
                index += 1;
            }
        }
    }
}



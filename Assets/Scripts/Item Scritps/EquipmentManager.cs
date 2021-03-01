using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ItemDatabase))]
public class EquipmentManager : MonoBehaviour
{
    [SerializeField]
    public List<Item> equipment = new List<Item>();
    ItemDatabase itemDatabase;
    const int eqSize = 32;
    public bool EqActive;

    [SerializeField]
    GameObject equipmentUI;
    [SerializeField]
    GameObject equippedItemsUI;
    [SerializeField]
    GameObject SlotPrefab;
    [SerializeField]
    GameObject SlotList;
    [SerializeField]
    EqSaver eqSaver;
    #region Character equipment variables
    public Item head, body, legs, boots, ringOne, ringTwo,
    weapon, offhand;
    [Tooltip("Boolean whether or not the inventory initialized from eqsaver")]
     bool initialized = false;
    #endregion

    #region EquippedItemsSlots
    [Header("Equipped item slots")]

    [SerializeField]
    GameObject headSlot, bodySlot, legsSlot, bootsSlot, RingOneSlot, ringTwoSlot, weaponSlot, offhandSlot;
    public GameObject[] slots;
    #endregion

    public PlayerFightScript playerfightscript;

    private void Start()
    {
        itemDatabase = GetComponent<ItemDatabase>();
        playerfightscript = GameObject.Find("Player").GetComponent<PlayerFightScript>();
        slots = new GameObject[] { headSlot, bodySlot, legsSlot, bootsSlot, RingOneSlot, ringTwoSlot, weaponSlot, offhandSlot };
    }

    public void LoadFromEqSaver()
    {
        itemDatabase = GetComponent<ItemDatabase>();
        playerfightscript = GameObject.Find("Player").GetComponent<PlayerFightScript>();
        eqSaver.Load();
        equipment = new List<Item>();
            int index = 0;
            foreach (int i in eqSaver.Equipment)
            {
                if (i != -1)
                {
                    AddItem(itemDatabase.Items[i]);
                    index++;
                Debug.LogWarning("Essa");
                }
            }

            LoadSavedEquippedItems();
            InitializeEquipment();
            initialized = true;

    }
    public void SaveEq()
    {
        if (initialized)
        {
            eqSaver.SaveEq(equipment);
            SaveEquippedItems();
            eqSaver.Save();
        }
        
    }
    void LoadSavedEquippedItems()
    {
        if (eqSaver.head != -1)
        {
            Equip(itemDatabase.Items[eqSaver.head]);
        }
        if (eqSaver.body != -1)
        {
            Equip(itemDatabase.Items[eqSaver.body]);
        }
        if (eqSaver.weapon != -1)
        {
            Equip(itemDatabase.Items[eqSaver.weapon]);
        }
        if (eqSaver.ringOne != -1)
        {
            Equip(itemDatabase.Items[eqSaver.ringOne]);
        }
        if (eqSaver.ringTwo != -1)
        {
            Equip(itemDatabase.Items[eqSaver.ringTwo]);
        }
     
    }
    void SaveEquippedItems()
    {
        if(head != null)
        {
            eqSaver.head = head.id;
        }
        else
        {
            eqSaver.head = -1;
        }
        if (body != null)
        {
            eqSaver.body = body.id;
        }
        else
        {
            eqSaver.body = -1;
        }
        if (weapon != null)
        {
            eqSaver.weapon = weapon.id;
        }
        else
        {
            eqSaver.weapon = -1;
        }
        if (ringOne != null)
        {
            eqSaver.ringOne = ringOne.id;
        }
        else
        {
            eqSaver.ringOne = -1;
        }
        if (ringTwo != null)
        {
            eqSaver.ringTwo = ringTwo.id;
        }
        else
        {
            eqSaver.ringTwo = -1;
        }
        if (offhand != null)
        {
            eqSaver.offhand = offhand.id;
        }
        else
        {
            eqSaver.offhand = -1;
        }

    }
   public void InitializeEquipment()
    {
        foreach (Transform child in SlotList.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < eqSize; i++)
        {
            GameObject param = Instantiate(SlotPrefab, SlotList.transform);
            param.name = i.ToString();


            if (i < equipment.Count)
                {
                    param.transform.GetChild(0).GetComponent<Image>().sprite = equipment[i].icon;
                    param.GetComponent<SlotScript>().item = equipment[i];
                }
                else
                {
                    param.transform.GetChild(0).GetComponent<Image>().color = new Color(0.0f,0.0f,0.0f,0.0f);
                }   
        }
        SaveEq();
        UpdateEquippedSlots();
    }

    //add item to inventory
    public void AddItem(Item item)
    {
        equipment.Add(item);
        InitializeEquipment();
    }
    //remove item from inventory
    public void RemoveItem(Item item)
    {
        if (initialized)
        {
            
                foreach (Item i in equipment)
                { 
                    if (i.id == item.id)
                    {
                        Debug.Log("Removed:" + i.id);
                        equipment.Remove(i);
                        InitializeEquipment();
                        SaveEq();
                        return;
                    }
                }
         
            SaveEq();
        }
    }

    public void Equip(Item item)
    {
        if (item != null)
        {
            switch (item.type)
            {
                case Type.body:
                    if (checkslot(body))
                    {
                        if (playerfightscript.Body != null)
                        {
                            Destroy(playerfightscript.Body);
                        }
                        body = item;
                        GameObject param = Instantiate(body.PrefabToSpawn, transform.position, Quaternion.identity, GameObject.Find("ArmorObject").transform);
                        playerfightscript.Body = param;
                        param.transform.localPosition = item.PrefabOffset;
                        param.transform.localEulerAngles = item.rot;
                        eqSaver.body = item.id;
                        RemoveItem(item);
                        
                    }
                    else
                    {
                        ChangeItemFromSlot(ref body, item);
                    }
                    break;
                case Type.head:
                    if (checkslot(head))
                    {
                        if (playerfightscript.Helmet != null)
                        {
                            Destroy(playerfightscript.Helmet);
                        }
                        head = item;
                        GameObject param = Instantiate(head.PrefabToSpawn, transform.position, Quaternion.identity, GameObject.Find("HelmetObject").transform);
                        playerfightscript.Helmet = param;
                        param.transform.localPosition = item.PrefabOffset;
                        param.transform.localEulerAngles = item.rot;
                        eqSaver.head = item.id;
                        RemoveItem(item);
                    }
                    else
                    {
                        ChangeItemFromSlot(ref head, item);
                    }
                    break;
             
                case Type.ring:
                    if (checkslot(ringOne))
                    {
                        ringOne = item;
                        eqSaver.ringOne = item.id;
                        RemoveItem(item);
                    }
                    else if (checkslot(ringTwo))
                    {
                        ringTwo = item;
                        eqSaver.ringTwo = item.id;
                        RemoveItem(item);
                    }
                    else
                    {
                        ChangeItemFromSlot(ref ringTwo, item);
                    }
                    break;
                case Type.weapon:
                    if (checkslot(weapon))
                    {
                        if (playerfightscript.helditem != null)
                        {
                            Destroy(playerfightscript.helditem);
                        }
                        weapon = item;
                        GameObject param = Instantiate(weapon.PrefabToSpawn, transform.position, Quaternion.identity, GameObject.Find("HoldingObject").transform);
                        playerfightscript.helditem = param;
                        param.transform.localPosition = item.PrefabOffset;
                        param.transform.localEulerAngles = item.rot;
                        eqSaver.weapon = item.id;
                        RemoveItem(item);
                    }
                    else
                    {
                        ChangeItemFromSlot(ref weapon, item);
                    }
                    break;
            }
        }
        InitializeEquipment();
        UpdateEquippedSlots();
        playerfightscript.UpdateStats();
        if (initialized)
        {
            SaveEq();
        }
    }
    //Checks if slot is empty (return true then)
    bool checkslot(Item slot)
    {
        if(slot == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

   public void ChangeItemFromSlot(ref Item slot,Item item)
    {
        if (slot != item)
        {
            AddItem(slot);
            slot = null;
            Equip(item);
            playerfightscript.UpdateStats();
        }
        SaveEq();
    }
    public void OpenUI()
    {
        if (!EqActive)
        {
            equipmentUI.SetActive(true);
            equippedItemsUI.SetActive(true);
            InitializeEquipment();
            EqActive = true;
        }
        else
        {
            equipmentUI.SetActive(false);
            equippedItemsUI.SetActive(false);
            EqActive = false;
            foreach (Transform child in SlotList.transform)
            {
                Destroy(child.gameObject);
            }
        }
        SaveEq();
    }
    public List<Item> EquippedItems()
    {
        List<Item> param = new List<Item>();
        param.Add(head);
        param.Add(body);
        param.Add(legs);
        param.Add(boots);
        param.Add(ringOne);
        param.Add(ringTwo);
        param.Add(weapon);
        param.Add(offhand);
        return param;
    }

   public void UpdateEquippedSlot(GameObject slot,Item item)
    {
        if (item != null)
        {
            slot.transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
            slot.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.icon;
        }
        else
        {
            slot.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
    }
    public void UpdateEquippedSlots()
    {
        UpdateEquippedSlot(headSlot, head);
        UpdateEquippedSlot(bodySlot, body);
        UpdateEquippedSlot(legsSlot, legs);
        UpdateEquippedSlot(bootsSlot, boots);
        UpdateEquippedSlot(weaponSlot, weapon);
        UpdateEquippedSlot(offhandSlot, offhand);
        UpdateEquippedSlot(RingOneSlot, ringOne);
        UpdateEquippedSlot(ringTwoSlot, ringTwo);
    }
    }

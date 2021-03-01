using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SlotScript : MonoBehaviour
{
    public Item item;
    EquipmentManager equipmentManager;
    [Tooltip("Prefab Of tooltip shown on hover")]
    public GameObject tooltipPrefab;

    #region displaying item info
    GameObject param;
    Text titleText;
    Text DescText;
    Image Icon;
    Text SpecialText;
    Text STRBonus, INTBonus, DEXBonus;
    #endregion
    private void Start()
    {
        equipmentManager = GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>();
    }
    public void OnHover()
    {
        if (item != null)
        {
            if (item.type != Type.usableItem)
            {
                param = Instantiate(tooltipPrefab, Vector3.zero, Quaternion.identity);
                param.transform.position = transform.position;
                param.transform.SetParent(GameObject.Find("EQUIPMENT UI").transform.GetChild(0).transform);
                titleText = param.transform.GetChild(1).gameObject.GetComponent<Text>();
                DescText = param.transform.GetChild(2).gameObject.GetComponent<Text>();
                SpecialText = param.transform.GetChild(3).gameObject.GetComponent<Text>();
                Icon = param.transform.GetChild(0).gameObject.GetComponent<Image>();
                STRBonus = param.transform.GetChild(7).gameObject.GetComponent<Text>();
                INTBonus = param.transform.GetChild(8).gameObject.GetComponent<Text>();
                DEXBonus = param.transform.GetChild(9).gameObject.GetComponent<Text>();
                titleText.text = item.title;
                DescText.text = item.descrpition;
                Icon.sprite = item.icon;
                if (item.type == Type.weapon)
                {
                    SpecialText.text = "Damage: " + item.stats["MinDMG"].ToString() + " - " + item.stats["MaxDMG"].ToString();
                }
                else
                {
                    SpecialText.text = "Armor: " + item.stats["Armor"];
                }
                if (item.stats.ContainsKey("BonusSTR"))
                {
                    STRBonus.text = item.stats["BonusSTR"].ToString();
                }
                if (item.stats.ContainsKey("BonusDEX"))
                {
                    DEXBonus.text = item.stats["BonusDEX"].ToString();
                }
                if (item.stats.ContainsKey("BonusINT"))
                {
                    INTBonus.text = item.stats["BonusINT"].ToString();
                }
            }
            else
            {
                param = Instantiate(tooltipPrefab, Vector3.zero, Quaternion.identity);
                param.transform.position = transform.position;
                param.transform.SetParent(GameObject.Find("EQUIPMENT UI").transform.GetChild(0).transform);
                titleText = param.transform.GetChild(1).gameObject.GetComponent<Text>();
                DescText = param.transform.GetChild(2).gameObject.GetComponent<Text>();
                SpecialText = param.transform.GetChild(3).gameObject.GetComponent<Text>();
                Icon = param.transform.GetChild(0).gameObject.GetComponent<Image>();
                titleText.text = item.title;
                DescText.text = item.descrpition;
                Icon.sprite = item.icon;
            }
        }
    }

    public void OnHoverLeave()
    {
        Destroy(param);
    }

    public void click()
    {
        if (item != null)
        {
            if (item.type != Type.usableItem)
            {
                Destroy(param);
                equipmentManager.Equip(item);
                Debug.Log(item.title + "Equipped");
            }
            else
            {
                equipmentManager.RemoveItem(item);
                string name = string.Concat(item.title.Where(c => !char.IsWhiteSpace(c)));
                equipmentManager.GetComponent<UsableItemBehaviors>().Invoke(name, 0f);
                Destroy(param);
            }
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopSlotScript : MonoBehaviour
{
    public int cost;
    public Item item;
    GameManager gm;
    EquipmentManager eq;
    public GameObject source;
    [SerializeField]
    GameObject tooltipPrefab;
    #region displaying item info
    GameObject param;
    Text titleText;
    Text DescText;
    Image Icon;
    Text SpecialText;
    Text STRBonus, INTBonus, DEXBonus;
    Text costText;
    #endregion
    public void InitializeIcon()
    {
        eq = GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>();
        gm = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        transform.GetChild(0).gameObject.GetComponent<Image>().sprite = item.icon;
    }
    public void Buy()
    {
        if(gm.Money >= cost)
        {
            
            gm.Money -= cost;
            eq.AddItem(item);
            int[] newitems = source.GetComponent<ShopScript>().items;
            for (int i = 0; i < newitems.Length; i++)
            {
                if (newitems[i] == item.id)
                {
                    newitems[i] = -1;
                    source.GetComponent<ShopScript>().costs[i] = -1;
                }
            }
            source.GetComponent<ShopScript>().items = newitems;
            Destroy(gameObject);
            Destroy(param);
        }
    }
   public void OnHover()
    {
        param = Instantiate(tooltipPrefab, Vector3.zero, Quaternion.identity);
        param.transform.position = transform.position;
        param.transform.SetParent(transform.parent.parent);
        titleText = param.transform.GetChild(1).gameObject.GetComponent<Text>();
        DescText = param.transform.GetChild(2).gameObject.GetComponent<Text>();
        SpecialText = param.transform.GetChild(3).gameObject.GetComponent<Text>();
        Icon = param.transform.GetChild(0).gameObject.GetComponent<Image>();
        STRBonus = param.transform.GetChild(7).gameObject.GetComponent<Text>();
        INTBonus = param.transform.GetChild(8).gameObject.GetComponent<Text>();
        DEXBonus = param.transform.GetChild(9).gameObject.GetComponent<Text>();
        costText = param.transform.GetChild(10).gameObject.GetComponent<Text>();
        costText.text = "Cost: "+cost.ToString();
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
    public void ExitHover()
    {
        Destroy(param);
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}

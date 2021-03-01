using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string title;
    public string description;
    public bool isActive;
    public bool Completed;
    public int ItemReward;
    public int GoldReward;
    public Quest(string _title,string _description,int item,int gold)
    {
        title = _title;
        description = _description;
        ItemReward = item;
        GoldReward = gold;
    }
}

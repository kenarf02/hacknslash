using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public int id;
    public string title;
    public string description;
    public bool isActive;
    public bool Completed;
    public int ItemReward;
    public int GoldReward;
    public Quest(int _id,string _title,string _description,int item,int gold)
    {
        id = _id;
        title = _title;
        description = _description;
        ItemReward = item;
        GoldReward = gold;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExpCategory 
{
    public string title;
    string description;
    public int currentlevel;
    public int expToNext; // Experience needed to level up in this category
    int exprequired;
   public ExpCategory(string _title, string _desc)
    {
        title = _title;
        description = _desc;
        expToNext = 10;
        exprequired = expToNext;
        currentlevel = 1;
    }
    public void levelUp()
    {
        Debug.LogError("Leveled up: " + title);
        currentlevel += 1;
        exprequired *= 3;
        expToNext = exprequired;
    }
    public void AddExp(int amount)
    {
        expToNext -= amount;
        if (expToNext <= 0&&currentlevel <20) {
            levelUp();
        }
    }
}

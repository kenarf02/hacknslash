using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCategory 
{
    string title;
    string description;
    public int currentlevel;
    int expToNext; // Experience needed to level up in this category
   public ExpCategory(string _title, string _desc)
    {
        title = _title;
        description = _desc;
        expToNext = 10;
        currentlevel = 1;
    }
    public void levelUp()
    {
        Debug.LogError("Leveled up: " + title);
        currentlevel += 1;
        expToNext *= 3;
    }
    public void AddExp(int amount)
    {
        expToNext -= amount;
        if (expToNext <= 0&&currentlevel <20) {
            levelUp();
        }
    }
}

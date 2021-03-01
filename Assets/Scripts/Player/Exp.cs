using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public List<ExpCategory> expCategories;
    GameManager gm;

    private void Awake()
    {
        gm = GetComponent<GameManager>();
        GetComponent<UIControl>().SetLevel(gm.Level);
        expCategories = new List<ExpCategory>()
        {
            new ExpCategory("Melee fighting",""),
            new ExpCategory("Spell casting", "")
        }; 
    }
   
    public void CheckforLevelUp()
    {
        int sum = 0;
        foreach(ExpCategory expCategory in expCategories)
        {
            sum += expCategory.currentlevel;
        }
        sum = Mathf.FloorToInt(sum / expCategories.Count);
        Debug.Log(sum);
        if(gm.Level < sum)
        {
            LevelUp();
        }
    }
    void LevelUp()
    {
        Debug.LogError("Level up");
        gm.Level += 1;
        GetComponent<UIControl>().SetLevel(gm.Level);
    }
}

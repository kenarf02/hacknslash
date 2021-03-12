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
        LoadExp();
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
        SaveExp();
    }
    void LevelUp()
    {
        Debug.LogError("Level up");
        gm.Level += 1;
        GetComponent<UIControl>().SetLevel(gm.Level);
    }

    void SaveExp()
    {
        foreach(ExpCategory category in expCategories)
        {
            PlayerPrefs.SetInt(category.title, category.currentlevel);
            PlayerPrefs.SetInt(category.title + "Remaining", category.expToNext);
        }
    }
    public void LoadExp()
    {
        foreach (ExpCategory category in expCategories)
        {

            category.currentlevel = PlayerPrefs.GetInt(category.title);
            category.expToNext = PlayerPrefs.GetInt(category.title + "Remaining");
        }
        SaveExp();
        CheckforLevelUp();
    }
}

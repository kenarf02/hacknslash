using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EqManager")]
public class EqSaver : ScriptableObject
{
    public int[] Equipment = new int[32];
    public int head, body, legs, boots, ringOne, ringTwo,
    weapon, offhand;
    public void SaveEq(List<Item> list)
    {
        int index = 0;
        for (int i = 0; i < Equipment.Length; i++)
        {
            if (i < list.Count)
            {
                if (list[i] != null)
                {
                    Equipment[index] = list[i].id;
                    index += 1;
                }
                else
                {
                    Equipment[index] = -1;
                }
            }
            else
            {
                Equipment[i] = -1;
            }
        }
        Save();
        Load();
    }
  
    public void Load()
    {
        if (PlayerPrefs.HasKey("EqInitialized"))
        {
            head = PlayerPrefs.GetInt("head");
            body = PlayerPrefs.GetInt("body");
            legs = PlayerPrefs.GetInt("legs");
            boots = PlayerPrefs.GetInt("boots");
            ringOne = PlayerPrefs.GetInt("ringOne");
            ringTwo = PlayerPrefs.GetInt("ringTwo");
            weapon = PlayerPrefs.GetInt("weapon");
            offhand = PlayerPrefs.GetInt("offhand");
            for (int i = 0; i < Equipment.Length; i++)
            {
                Equipment[i] = PlayerPrefs.GetInt(i.ToString());
            }
        }
        else
        {
            SetDefaultPlayerPrefs();
            head = PlayerPrefs.GetInt("head");
            body = PlayerPrefs.GetInt("body");
            legs = PlayerPrefs.GetInt("legs");
            boots = PlayerPrefs.GetInt("boots");
            ringOne = PlayerPrefs.GetInt("ringOne");
            ringTwo = PlayerPrefs.GetInt("ringTwo");
            weapon = PlayerPrefs.GetInt("weapon");
            offhand = PlayerPrefs.GetInt("offhand");
            for (int i = 0; i < Equipment.Length; i++)
            {
                Equipment[i] = PlayerPrefs.GetInt(i.ToString());
            }
        }
    }
    public void Save()
    {
        PlayerPrefs.SetInt("head", head);
        PlayerPrefs.SetInt("body", body);
        PlayerPrefs.SetInt("legs", legs);
        PlayerPrefs.SetInt("boots", boots);
        PlayerPrefs.SetInt("ringOne", ringOne);
        PlayerPrefs.SetInt("ringTwo", ringTwo);
        PlayerPrefs.SetInt("weapon", weapon);
        PlayerPrefs.SetInt("offhand", offhand);
        for (int i = 0; i < Equipment.Length; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), Equipment[i]);
        }
        PlayerPrefs.Save();
    }
    public void SetDefaultPlayerPrefs()
    {
        PlayerPrefs.SetInt("EqInitialized", 1);
        PlayerPrefs.SetInt("head", -1);
        PlayerPrefs.SetInt("body", -1);
        PlayerPrefs.SetInt("legs", -1);
        PlayerPrefs.SetInt("boots", -1);
        PlayerPrefs.SetInt("ringOne", -1);
        PlayerPrefs.SetInt("ringTwo", -1);
        PlayerPrefs.SetInt("weapon", -1);
        PlayerPrefs.SetInt("offhand", -1);
        for (int i = 0; i < Equipment.Length; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), -1);
        }
    }
}
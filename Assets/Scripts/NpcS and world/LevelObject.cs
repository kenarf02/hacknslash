using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level manager")]
public class LevelObject : ScriptableObject
{
    public Vector3 OverworldPosition;
    public int InsideLevel;
    public int Playerlevel;
    public int Health, MaxHealth;
    public int Mana, MaxMana;
    public int Money;
    
    public void Save()
    {
        PlayerPrefs.SetFloat("XPos", OverworldPosition.x);
        PlayerPrefs.SetFloat("YPos", OverworldPosition.y);
        PlayerPrefs.SetFloat("ZPos", OverworldPosition.z);
        PlayerPrefs.SetInt("InsideLevel", InsideLevel);
        PlayerPrefs.SetInt("PlayerLevel", Playerlevel);
        PlayerPrefs.SetInt("Health", Health);
        PlayerPrefs.SetInt("MaxHealth", MaxHealth);
        PlayerPrefs.SetInt("Mana", Mana);
        PlayerPrefs.SetInt("MaxMana", MaxMana);
        PlayerPrefs.SetInt("Money", Money);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("LevelInitialized"))
        {
            OverworldPosition = new Vector3(PlayerPrefs.GetFloat("XPos"), PlayerPrefs.GetFloat("YPos"), PlayerPrefs.GetFloat("ZPos"));
            InsideLevel = PlayerPrefs.GetInt("InsideLevel");
            Playerlevel = PlayerPrefs.GetInt("PlayerLevel");
            Health = PlayerPrefs.GetInt("Health");
            MaxHealth = PlayerPrefs.GetInt("MaxHealth");
            Mana = PlayerPrefs.GetInt("Mana");
            MaxMana = PlayerPrefs.GetInt("MaxMana");
            Money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            SetDefaultPlayerPrefs();
        }
    }
    public void SetDefaultPlayerPrefs()
    {
        PlayerPrefs.SetInt("LevelInitialized", 1);
        PlayerPrefs.SetFloat("XPos", 0);
        PlayerPrefs.SetFloat("YPos", 0);
        PlayerPrefs.SetFloat("ZPos", 0);
        PlayerPrefs.SetInt("InsideLevel", 0);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("Health", 10);
        PlayerPrefs.SetInt("MaxHealth", 10);
        PlayerPrefs.SetInt("Mana", 10);
        PlayerPrefs.SetInt("MaxMana", 10);
        PlayerPrefs.SetInt("Money", 100);
        PlayerPrefs.Save();
        Load();
    }
}


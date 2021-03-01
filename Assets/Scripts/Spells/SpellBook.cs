using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
  public List<Spell> SpellDataBase;
    #region Prefabs and variables

    #endregion
    private void Awake()
    {
        buildDataBase();
        
    }
    void buildDataBase()
    {
        SpellDataBase = new List<Spell>() { 
            gameObject.AddComponent<FireBall>(),
            gameObject.AddComponent<ZombieHands>(),
            gameObject.AddComponent<Heal>()
        };
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlotBehavior : MonoBehaviour
{
    public int spell;
    SpellBookUI spellBookUI;
    private void Start()
    {
        spellBookUI = GameObject.Find("Spell ui").GetComponent<SpellBookUI>();
    }
    public void OnClick()
    {
        spellBookUI.AddSpell(spell);
    }
}

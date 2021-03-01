using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSlotBehavior : MonoBehaviour
{
    EquipmentManager eq;
    PlayerFightScript playerfightscript;
    private void Start()
    {
        eq = GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>();
        playerfightscript = GameObject.Find("Player").GetComponent<PlayerFightScript>();
    }
    public void Clicked()
    {
        //
        switch (transform.parent.name)
        {
            case "head":
                eq.ChangeItemFromSlot(ref eq.head, null);
                Destroy(playerfightscript.Helmet);
                break;
            case "body":
                eq.ChangeItemFromSlot(ref eq.body, null);
                Destroy(playerfightscript.Body);
                break;
            case "offhand":
                eq.ChangeItemFromSlot(ref eq.offhand, null);
                break;
            case "weapon":
                eq.ChangeItemFromSlot(ref eq.weapon, null);
                if (playerfightscript.helditem != null)
                {
                    Destroy(playerfightscript.helditem);
                }
                break;
            case "legs":
                eq.ChangeItemFromSlot(ref eq.legs, null);
                break;
            case "boots":
                eq.ChangeItemFromSlot(ref eq.boots, null);
                break;
            case "ringOne":
                eq.ChangeItemFromSlot(ref eq.ringOne, null);
                break;
            case "ringTwo":
                eq.ChangeItemFromSlot(ref eq.weapon, null);
                break;
        }
    }
}

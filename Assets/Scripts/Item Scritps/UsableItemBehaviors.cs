using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItemBehaviors : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab for froggyspawner item")]
    GameObject froggyfriendprefab;
    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    public void Froggyspawner()
    {
        Instantiate(froggyfriendprefab, GameObject.Find("PetFollowPoint").transform.position, Quaternion.identity);
    }
    public void MinorPotionofHealing()
    {
        int randomAmount = Random.Range(2, 10);
        player.GetComponent<PlayerFightScript>().Heal(randomAmount);
    }
}

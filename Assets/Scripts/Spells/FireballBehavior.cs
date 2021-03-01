using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    public int damage;
    public Transform target;
    [SerializeField]
    float Speed;
    private void Update()
    {
       transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Speed*Time.deltaTime);
        if(transform.position == target.transform.position)
        {
            Destroy(gameObject);
            target.gameObject.GetComponent<EnemyScript>().GetDamage(damage);
            GameObject.Find("GAME MANAGER").GetComponent<GameManager>().PlayerCanWalk = true;
        }
    }
}

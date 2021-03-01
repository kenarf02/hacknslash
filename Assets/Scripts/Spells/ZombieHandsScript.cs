using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHandsScript : MonoBehaviour
{
    public int Damage;
    public float destroyTime;
    Collider[] hitColliders;
    GameObject[] Enemies;
    [SerializeField]
    float radius;
    [SerializeField]
    float dmgspeed;
    float timer;
    void Start()
    {
        timer = dmgspeed;
        Destroy(gameObject, destroyTime);
    }
    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider col in hitColliders)
        {
            if (col.tag == "Enemy")
            {
                if (timer <= 0)
                {
                    col.gameObject.GetComponent<EnemyScript>().GetDamage(Damage);
                    timer = dmgspeed;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }
    }
    
}

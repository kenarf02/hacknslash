using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum state
{
    movestate,
    chargestate,
    attackstate,
    ConfusionState,
    idle
}
public class GoblinBoss : MonoBehaviour
{
    bool attacking;
    [SerializeField]
    state currstate;
    Transform player;
    [SerializeField]
    float movespeed;
    [SerializeField]
    int Damage;
    int Health;
    float confusionTimer;
    [SerializeField]
    float ConfusionTime;
    Animator anim;
    [SerializeField]
    GameObject attackSpot;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;   
    }
    private void Update()
    {
        //switch between states
        if (currstate == state.chargestate)
        {
            Charge();
        } else if (currstate == state.attackstate)
        {
            if (!attacking)
            {
                Attack();
            }
        }
        else if (currstate == state.movestate)
        {
            Move();
        } 
        else if (currstate == state.idle)
        {
            Idle();
        }
        else
        {
            Confusion();
        }
    }
    void Idle()
    {
        anim.SetBool("Charging", false);
        //anim.SetTrigger("AttackFinished");
    }
    void Charge()
    {
        anim.SetBool("Charge", true);
    }
    void Attack()
    {
        attacking = true;
        anim.SetTrigger("Attack");
        SpawnAttackSpots();
        StartCoroutine(attackTime(3f));
    }
    void Move()
    {
        anim.SetBool("Charging", true);
    }
    void Confusion()
    {
        anim.SetBool("Charging", false) ;
    }
   
    void RandomizeStates()
    {

    }
    void SpawnAttackSpots()
    {
        int amount = Random.Range(3, 7);
        for (int i = 0; i < amount; i++) {
           GameObject param = Instantiate(attackSpot, transform.position + new Vector3(Random.Range(-15,15),-1.5f,Random.Range(-15,15)),Quaternion.identity,transform.parent);
            param.GetComponent<CircleIndicator>().DoRenderer();
        }
    }
    IEnumerator attackTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.SetTrigger("AttackFinished");
        currstate = state.idle;
        attacking = false;
        Debug.LogError("Stopped coroutine");
        StopAllCoroutines();
        yield return null;
    }
}

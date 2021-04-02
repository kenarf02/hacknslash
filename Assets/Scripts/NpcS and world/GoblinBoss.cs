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
            Attack();
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
        anim.SetBool("Charge", false);
        anim.SetTrigger("AttackFinished");
    }
    void Charge()
    {
        anim.SetBool("Charge", true);
    }
    void Attack()
    {
        anim.SetTrigger("Attack");
    }
    void Move()
    {
        anim.SetBool("Charge",true);
    }
    void Confusion()
    {
        anim.SetBool("Charge", false) ;
        anim.SetTrigger("AttackFinished");
    }
}

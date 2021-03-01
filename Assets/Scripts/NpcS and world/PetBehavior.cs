using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PetBehavior : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    Animator anim;
    void Start()
    {
        player = GameObject.Find("PetFollowPoint");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (agent.hasPath)
        {
            anim.SetBool("ismoving", true);
        }
        else
        {
            anim.SetBool("ismoving", false); ;
        }

        if ((transform.position - player.transform.position).magnitude >= 2)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}

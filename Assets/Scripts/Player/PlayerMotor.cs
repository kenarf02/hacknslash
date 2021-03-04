using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{

    NavMeshAgent agent;
    GameManager gameManager;
    Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
        anim = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
    private void Update()
    {
        if (agent.velocity.magnitude != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

    }
    public void MoveToPoint(Vector3 point){
        if (gameManager.PlayerCanWalk)
        {
            agent.SetDestination(point);
        }
    }
    public void StopMovement()
    {
        agent.destination = gameObject.transform.localPosition;
    }
}

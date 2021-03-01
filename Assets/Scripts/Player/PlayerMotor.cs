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
        if (!agent.hasPath)
        {
            anim.SetBool("Run", false);
        
        }
    }
    public void MoveToPoint(Vector3 point){
        if (gameManager.PlayerCanWalk)
        {
            agent.SetDestination(point);
            anim.SetBool("Run",true);
           
        }
    }
    public void StopMovement()
    {
        agent.destination = gameObject.transform.localPosition;
    }
}

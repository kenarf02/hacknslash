using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FroggyScript : MonoBehaviour
{
    [SerializeField]
    GameObject nametext;
    GameObject cammain;
    Animator anim;
    Vector3 point;
    NavMeshAgent agent;
    private void Start()
    {
        cammain = Camera.main.gameObject;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
;    }
    private void Update()
    {
        nametext.transform.LookAt(cammain.transform);
        Move();
    }
    private void OnMouseDown()
    {
        if (GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>().Quests[0].Completed != true && GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>().Quests[0].isActive == true) {
            if ((transform.position - GameObject.Find("Player").transform.position).magnitude <= 4)
            {
                GameObject.Find("QUEST MANAGER").GetComponent<QuestDatabase>().Quests[0].Completed = true;
                Debug.Log("Froggy quest completed");
                GameObject.Find("Gabrysia").GetComponent<NpcStartDialogue>().thisNpcDialogue = GameObject.Find("Gabrysia").GetComponent<QuestGiverDialogueLines>().QuestDone;
                //TODO: ADD SOME PARTICLES WHEN FROG DISAPPEARS
                Destroy(gameObject);
            }
        }
    }
    private void Move()
    {
        if (agent.hasPath)
        {
            anim.SetBool("ismoving", true);
        }
        else
        {
            anim.SetBool("ismoving", false);
            StartCoroutine(changePoint());
        }
    }
    IEnumerator changePoint()
    {
        yield return new WaitForSeconds(2f);
        point = new Vector3(transform.position.x + Random.Range(-2, 2), 0, transform.position.z + Random.Range(-2, 2));
        agent.SetDestination(point);
        StopAllCoroutines();
    }
}

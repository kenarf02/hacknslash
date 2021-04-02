using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(PlayerFightScript))]
public class PlayerController : MonoBehaviour
{
    public LayerMask movementmask;
    PlayerMotor motor;
    Camera cam;
    public GameObject target_circle_prefab;
    public float distanceForDialogue =3f;
    PlayerFightScript playerFightScript;
    EquipmentManager equipmentManager;
    public bool isCasting = false;
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        playerFightScript = GetComponent<PlayerFightScript>();
        equipmentManager = GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100, movementmask))
                {
                if (hit.collider.tag == "Npc")
                    {
                        if ((transform.position-hit.collider.gameObject.transform.position).magnitude < distanceForDialogue)
                        {
                            transform.LookAt(hit.collider.gameObject.transform);
                            hit.collider.gameObject.GetComponent<NpcStartDialogue>().StartDialogue();
                            motor.MoveToPoint(transform.position);
                        }
                        else
                        {
                            motor.MoveToPoint(hit.point);
                        }
                    playerFightScript.DeleteCircle();
                    if (!playerFightScript.spellbeingcast)
                    {
                        isCasting = false;
                    }
                    }

                if (hit.collider.tag == "Enemy")
                {
                    
                    if (GameObject.FindGameObjectWithTag("Circle"))
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Circle"));
                    }
                     Debug.LogError("Target aquired");
                        GetComponent<PlayerFightScript>().target = hit.collider.gameObject;
                        GameObject target = hit.collider.gameObject;
                        GameObject Circle = Instantiate(target_circle_prefab, target.transform.position + new Vector3(0, -0.97f, 0), Quaternion.identity, hit.collider.gameObject.transform);
                        Circle.GetComponent<CircleIndicator>().DoRenderer();
                    
                }
                else
                {
                   
                    if (GameObject.FindGameObjectWithTag("Circle"))
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Circle"));
                    }
                    if (!playerFightScript.spellbeingcast)
                    {
                        isCasting = false;
                        playerFightScript.target = null;
                    }
                }


                if (hit.collider.tag == "Pickup")
                    {
                        equipmentManager.AddItem(hit.collider.gameObject.GetComponent<DropItemScript>().item);
                        Debug.LogError("Picked up" + hit.collider.gameObject.GetComponent<DropItemScript>().item.title);
                        Destroy(hit.collider.gameObject);
                        playerFightScript.DeleteCircle();
                  
                }
                if(hit.collider.tag == "Ground")
                {
                    playerFightScript.DeleteCircle();
                }
                motor.MoveToPoint(hit.point);

            }
                
            
        }
    }
}

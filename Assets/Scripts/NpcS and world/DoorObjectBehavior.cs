using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorObjectBehavior : MonoBehaviour
{
    [SerializeField]
    int LevelToMove;
    [SerializeField]
    LevelObject levelobj;
    [SerializeField]
    GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("GAME MANAGER").GetComponent<GameManager>();
    }
    private void OnMouseDown()
    {
        if ((GameObject.Find("Player").transform.position - transform.position).magnitude <3)
        {
            if (LevelToMove != -1)
            {
                levelobj.InsideLevel = LevelToMove;
                SceneManager.LoadScene(1);
                GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>().SaveEq();
                gm.Savestats();
                gm.Save();
            }
            else
            {
                gm.Savestats();
                GameObject.Find("EQUIPMENT MANAGER").GetComponent<EquipmentManager>().SaveEq();
                SceneManager.LoadScene(0);
            }
        }
    }
}

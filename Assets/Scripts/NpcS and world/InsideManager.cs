using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InsideManager : MonoBehaviour
{
    [SerializeField]
    LevelObject levelobj;
    [SerializeField]
    GameObject[] levels;
    public GameObject currentlevel;
    [SerializeField]
    GameObject player;
    private void Start()
    {
        Spawn();   
    }
    public void Spawn()
    {
        currentlevel = levels[levelobj.InsideLevel];
        currentlevel.SetActive(true);
        player.transform.position = currentlevel.transform.Find("Spawn").position;
    }
    public void Despawn()
    {
        SceneManager.LoadScene(0);   
    }
}

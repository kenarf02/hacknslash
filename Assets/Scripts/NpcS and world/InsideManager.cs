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
        foreach(GameObject level in levels)
        {
            level.SetActive(false);
        }
        currentlevel = levels[levelobj.InsideLevel];
        currentlevel.SetActive(true);
        player.transform.position = currentlevel.transform.Find("Spawn").position;
        Debug.Log(currentlevel.transform.Find("Spawn").position);
    }
    public void Despawn()
    {
        SceneManager.LoadScene(0);   
    }
}

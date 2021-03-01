using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopNumberScript : MonoBehaviour
{
    GameObject cam;
    private void Start()
    {
        cam = Camera.main.gameObject;
        Destroy(gameObject, 4f);
    }
    void Update()
    {
        transform.GetChild(0).transform.LookAt(cam.transform);      
    }
}

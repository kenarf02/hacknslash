using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyScript))]
public class EnemyUIScript : MonoBehaviour
{
    Slider slider;
    EnemyScript scr;
    GameObject cam;
    public string NameString;
    public Text NameText;
    private void Update()
    {
        slider.transform.LookAt(cam.transform);
        NameText.transform.LookAt(cam.transform);
    }
    private void Start()
    {
        slider = gameObject.GetComponentInChildren(typeof(Slider)) as Slider;
        scr = GetComponent<EnemyScript>();
        cam = Camera.main.gameObject;
        slider.maxValue = scr.HP;
        slider.value = scr.HP;
        NameText.text = NameString;
    }
    public void UpdateHealthBar(int HP)
    {
        slider.value = HP;
    }
}

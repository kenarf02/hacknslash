using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    head,body,ring,
    weapon,usable,neutral,usableItem
}


public class Item
{
    public int id;
    public string title;
    public string descrpition;
    public Sprite icon;
    public Type type;
    public Dictionary<string, int> stats = new Dictionary<string, int>();

    #region Spawning prefab
    public GameObject PrefabToSpawn;
    public Vector3 rot;
    public Vector3 PrefabOffset;
    #endregion
    public Item(int _id, string _title, string _description, Type _type, Vector3 _rot, Vector3 _PrefabOffset,
                Dictionary<string, int> _stats)
    {
        id = _id;
        title = _title;
        descrpition = _description;
        icon = Resources.Load<Sprite>("Sprites/Items/" + title);
        type = _type;
        stats = _stats;
        PrefabToSpawn = Resources.Load<GameObject>("ItemModels/" + title);
        rot = _rot;
        PrefabOffset = _PrefabOffset;
    }

    public Item(Item item)
    {
        id = item.id;
        title = item.title;
        descrpition = item.descrpition;
        icon = Resources.Load<Sprite>("Sprites/Items/" + item.title);
        stats = item.stats;
    }
}
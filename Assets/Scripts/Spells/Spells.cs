using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells: MonoBehaviour
{

}
public class Spell : MonoBehaviour
{
    public float range;
    public int manacost;
    public float castTime;
    protected PlayerFightScript playerFightScript;
    protected GameObject Player;
    public string Title;
    public string Description;
    public float cooldown;
    public Sprite icon;
    public Spell(string _Title, string _Description,float _range, int _manacost, float _castTime,float _cooldown)
    {
        Title = _Title;
        Description = _Description;
        manacost = _manacost;
        range = _range;
        castTime = _castTime;
        cooldown = _cooldown;
    }
    public virtual void OnUse(GameObject Target, int Damage)
    {
        Player = GameObject.Find("Player");
        playerFightScript = Player.GetComponent<PlayerFightScript>();
    }

}

public class FireBall : Spell
{
    GameObject Prefab;
    public FireBall() : base("Fire ball","",10f, 2, 1f,2f)
    {
    }
    private void Awake()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/Fireball_Prefab");
        icon = Resources.Load<Sprite>("Sprites/Spells/" + Title);
    }
    public override void OnUse(GameObject Target, int Damage)
    {
        base.OnUse(Target, Damage);
        Debug.Log("Fireball");
       GameObject ball = Instantiate(Prefab,GameObject.Find("HoldingObject").transform.position,Quaternion.identity);
        ball.GetComponent<FireballBehavior>().damage = Damage;
        ball.GetComponent<FireballBehavior>().target = Target.transform;
    }

}


public class ZombieHands : Spell
{
    GameObject Prefab;

    public ZombieHands() : base("Zombie hands", "",10f, 2, 0.5f,2f)
    {
    }
    private void Awake()
    {
        Prefab = Resources.Load<GameObject>("Prefabs/ZombieHandsPrefab");
        icon = Resources.Load<Sprite>("Sprites/Spells/" + Title);
    }
    public override void OnUse(GameObject Target, int Damage)
    {
        base.OnUse(Target, Damage);
        Debug.Log("ZombieHands");
        GameObject handsObject = Instantiate(Prefab, Target.transform.position + new Vector3(0,-1f,0), Quaternion.identity);
        handsObject.GetComponent<ZombieHandsScript>().Damage = Damage;
    }
}

public class Heal : Spell
{
    GameObject prefab;
    public Heal() : base("Heal", "",0f, 2, 0.5f,2f)
    {
    }
    private void Awake()
    {
        icon = Resources.Load<Sprite>("Sprites/Spells/" + Title);
    }
    public override void OnUse(GameObject Target, int Damage)
    {
        base.OnUse(Target, Damage);
        playerFightScript.Heal(Damage);
    }
}
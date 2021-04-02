using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    public void BuildDatabase()
    {
        Items = new List<Item> { 
            new Item(0, "Wooden Sword", "A sword of bad quality made of wood",Type.weapon,new Vector3(0, 90, 90),new Vector3(0, 0, -0.0157f), new Dictionary<string, int>
            {
                {"MaxDMG",4},
                {"MinDMG",1},
                {"BonusSTR",Random.Range(0,2)},
                {"BonusDEX",Random.Range(0,2)},
                {"BonusINT",0},
                {"AttackSpeed",1},
                {"AttackRange",2 }
            }),
            new Item(1, "Iron Sword", "A sword of better quality made of Iron",Type.weapon,new Vector3(0, 90, 90),new Vector3(0, 0, -0.0157f), new Dictionary<string, int>
            {
                {"MaxDMG",8},
                {"MinDMG",2},
                {"BonusSTR",Random.Range(0,4)},
                {"BonusDEX",Random.Range(0,1)},
                {"BonusINT",0},
                {"AttackSpeed",1},
                {"AttackRange",2 }
            }),new Item(2, "Club", "Primitive, yet dangerous",Type.weapon,new Vector3(0, 0, 180.744f),new Vector3(0, 0, -0.004648845f),new Dictionary<string, int>
            {
                {"MaxDMG",4},
                {"MinDMG",2},
                {"BonusSTR",2},
                {"BonusDEX",0},
                {"BonusINT",0},
                {"AttackSpeed",1},
                {"AttackRange",2 }
            }), new Item(3, "Ivros, the dragon slayer", "Forged in deepest dwarven forges",Type.weapon,new Vector3(0, -90, 90),new Vector3(0, 0, 0.026f),new Dictionary<string, int>
            {
                {"MaxDMG",20},
                {"MinDMG",10},
                {"BonusSTR",5},
                {"BonusDEX",5},
                {"BonusINT",5},
                {"AttackSpeed",2},
                {"AttackRange",3 }
            }),new Item(4, "Bikat, the warhammer", "Forged in deepest dwarven forges",Type.weapon,new Vector3(0, 90, 90),new Vector3(-0.0016f, -0.0025f, 0.0053f),new Dictionary<string, int>
            {
                {"MaxDMG",20},
                {"MinDMG",10},
                {"BonusSTR",5},
                {"BonusDEX",5},
                {"BonusINT",5},
                {"AttackSpeed",2},
                {"AttackRange",3 }
            }), new Item(5,"Iron helmet","Helmet providing basic protection",Type.head,new Vector3(-90f,0,0),new Vector3(0,-0.0812f,-0.0053f),new Dictionary<string, int>
            {
                {"Armor",10},
                {"BonusSTR",1},
                {"BonusDEX",1},
                {"BonusINT",1},
            }),new Item(6,"Iron chestplate","Chestplate providing basic protection",Type.body,new Vector3(-90f,0,0),new Vector3(0,-0.06f,-0.0048f),new Dictionary<string, int>
            {
                {"Armor",10},
                {"BonusSTR",1},
                {"BonusDEX",1},
                {"BonusINT",1},
            }), 
            new Item(7, "Froggy spawner", "Spawns a froggy friend",Type.usableItem,Vector3.zero,Vector3.zero,null),
            new Item(8,"Xyndrinas","It is said gods were killed with Xyndrinas",Type.weapon,new Vector3(0,-90f,90f),new Vector3(0.0011f,-0.0003f,0.0312f),new Dictionary<string, int>
            {
                {"MaxDMG",20},
                {"MinDMG",10},
                {"BonusSTR",5},
                {"BonusDEX",5},
                {"BonusINT",5},
                {"AttackSpeed",2},
                {"AttackRange",3 }
            }),
            new Item(9, "Minor Potion of Healing","Heals small amount of health",Type.usableItem,Vector3.zero,Vector3.zero,null),
            new Item(10,"Pickaxe","Used for mining and killing monsters",Type.weapon,new Vector3(0,0,90f),new Vector3(0.0008827197f,0.000877101f,-0.0134993f),new Dictionary<string, int>
            {
                {"MaxDMG",1},
                {"MinDMG",4},
                {"BonusSTR",5},
                {"BonusDEX",5},
                {"BonusINT",5},
                {"AttackSpeed",2},
                {"AttackRange",3 }
            }),
            new Item(11,"Iron Warhammer","Dangarous weapon in hands of a strong warrior",Type.weapon,new Vector3(0,0,90f),new Vector3(0.0007f,0f,0.0036f),new Dictionary<string, int>
            {
                {"MaxDMG",1},
                {"MinDMG",4},
                {"BonusSTR",5},
                {"BonusDEX",5},
                {"BonusINT",5},
                {"AttackSpeed",2},
                {"AttackRange",3 }
            }),
            new Item(12,"Goblin brute chestplate","You might think it's too big, yet it fits perfectly",Type.body,new Vector3(-90f,0,0),new Vector3(0,-0.06f,-0.0048f),new Dictionary<string, int>
            {
                {"Armor",10},
                {"BonusSTR",1},
                {"BonusDEX",1},
                {"BonusINT",1},
            }),
            };
    }
    Item GetItem(string itemtitle)
    {
        return Items.Find(item => item.title == itemtitle);
    }
  
}


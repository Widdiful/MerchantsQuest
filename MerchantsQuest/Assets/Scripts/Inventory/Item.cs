using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Spell, Weapon, Armor, Consumable }

//If the item is a spell type then you auto learn and consume
public struct Item
{
    public int id;
    public string name;
    public string fakeDescription;
    public float realStat;
    public bool appraised;
    public ItemType type;
    public Texture2D icon;
}

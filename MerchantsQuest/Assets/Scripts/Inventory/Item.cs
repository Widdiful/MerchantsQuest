using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Spell, Weapon, Armor, Consumable, Size }
public enum StatType { None, Health, Magic, Attack, Defence, Agility, Size}
public enum SpellType { None, Heal, Damage, Physical, Size }
//If the item is a spell type then you auto learn and consume
[System.Serializable]
public struct Item
{
    public int id;
    public string name;
    public string fakeDescription;
    public bool appraised;

    public ItemType type;

    public StatType primaryStat;
    public int primaryStatValue;
    public StatType secondaryStat;
    public int secondaryStatValue;

    public Sprite icon;

    public SpellType spellType;
    public int manaCost;

    public int price;
}

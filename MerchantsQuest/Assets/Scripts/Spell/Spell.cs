using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Spell
{
    public int id;
    public string name;

    public SpellType spellType;
    public int primaryStatValue;
    public int manaCost;

    public Sprite icon;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Spell
{
    public int id;
    public bool appraised;

    public string name;
    public string fakeDescription;
    public SpellType spellType;
    public int primaryStatValue;
    public int manaCost;
    public bool lifesteal;

    public Sprite icon;
    public Sprite activeIcon;
}

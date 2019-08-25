﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    public static SpellInventory Instance;

    public SpellSlot[] spellSlots;

    public EquippedSpells equipedSpells;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    public void AddSpell(Spell spell)
    {
        for(int i = 0; i <spellSlots.Length; i++)
        {
            if(spellSlots[i].empty)
            {
                spellSlots[i].SetSpell(spell);
                return;
            }
        }
        Debug.Log("Spell inventory is full");
    }

    public void RemoveSpell(int id)
    {
        for(int i = 0; i < spellSlots.Length; i++)
        {
            if(spellSlots[i].spellInSlot.id == id)
            {
                spellSlots[i].RemoveSpell();
                return;
            }
        }
    }
}
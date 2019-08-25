using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSpells : MonoBehaviour
{

    public SpellSlot[] spells;

    public void AddToSlot(Spell spell)
    {
        for(int i  =0; i < spells.Length; i++)
        {
            if(spells[i].empty)
            {
                spells[i].SetSpell(spell);
            }
        }
    }

    public void RemoveFromSlot(int id)
    {
        for(int i = 0; i < spells.Length; i++)
        {
            if(spells[i].spellInSlot.id == id)
            {
                spells[i].RemoveSpell();
            }
        }
    }
}

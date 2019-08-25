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
                return;
            }
        }
    }

    public void RemoveFromSlot(SpellSlot slot)
    {
        for(int i = 0; i < spells.Length; i++)
        {
            if(spells[i] == slot)
            {
                SpellInventory.Instance.AddSpell(spells[i].spellInSlot);
                spells[i].RemoveSpell();
                return;
            }
        }
    }
}

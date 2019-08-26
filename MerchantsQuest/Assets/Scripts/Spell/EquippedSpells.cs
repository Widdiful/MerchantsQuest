using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSpells : MonoBehaviour
{

    public SpellSlot[] spells;

    public void AddToSlot(Spell spell)
    {
        for(int i = 0; i < spells.Length; i++)
        {
            if(spells[i].empty)
            {
                spells[i].SetSlot(spell);
                return;
            }
        }
    }

    public void RemoveFromSlot(SpellSlot slot)
    {
        SpellInventory.Instance.AddSpell(slot.item);
        slot.RemoveItem();
    }
}

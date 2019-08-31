using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInventory : MonoBehaviour
{
    public static SpellInventory Instance;

    public SpellSlot[] spellSlots;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }


    public void AddSpell(Spell spell)
    {
        for(int i = 0; i <spellSlots.Length; i++)
        {
            if(spellSlots[i].empty)
            {
                spellSlots[i].SetSlot(spell);
                
                for(int j =0; j< PartyManager.instance.partyMembers.Count; j++)
                {
                    PartyManager.instance.partyMembers[j].spellList.Add(spell);
                }
                return;
            }
        }
        Debug.Log("Spell inventory is full");
    }

    public void RemoveSpell(int id)
    {
        for(int i = 0; i < spellSlots.Length; i++)
        {
            if(spellSlots[i].item.id == id)
            {
                spellSlots[i].RemoveItem();
                return;
            }
        }
    }

    public void AppraiseSpell(int id)
    {
        for(int i = 0; i < spellSlots.Length; i++)
        {
            if(spellSlots[i].item.id == id)
            {
                spellSlots[i].item.appraised = true;
            }
        }
    }
}

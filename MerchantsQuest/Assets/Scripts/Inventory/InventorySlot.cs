using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : Slot<Item>
{

    public new void SetSlot(Item newItem)
    {
        base.SetSlot(newItem);
        icon.sprite = item.icon;
    }

    public override void DisplayStats()
    {
        if(!empty)
            InventoryManager.Instance.DisplayStats(item, transform.position);
    }


    public void EquipItem()
    {
        if(item.type == ItemType.Spell)
        {
            Spell newSpell = new Spell();
            newSpell.id = item.id;
            newSpell.manaCost = item.manaCost;
            newSpell.spellType = item.spellType;
            newSpell.primaryStatValue = item.primaryStatValue;
            newSpell.icon = item.icon;
            SpellInventory.Instance.AddSpell(newSpell);
        }
        else
            InventoryManager.Instance.equipment.AddToSlot(item, item.type);


        RemoveItem();
    }
}


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : Slot<Spell>
{

    private void Awake()
    {
        if (icon.sprite == null)
            icon.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        empty = true;
    }

    public new void SetSlot(Spell newItem)
    {
        base.SetSlot(newItem);
        icon.sprite = item.icon;
    }


    public void EquipSpell()
    {
        SpellInventory.Instance.equipedSpells.AddToSlot(item);
        RemoveItem();
    }

    public override void DisplayStats()
    {
        if(!empty)
            InventoryManager.Instance.statShower.SetSpellStats(item, transform.position);
    }

}

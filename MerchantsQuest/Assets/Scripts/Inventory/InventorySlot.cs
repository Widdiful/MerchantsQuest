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

    public new void RemoveItem()
    {
        icon.sprite = null;
        icon.color = new Color(.0f, .0f, .0f, .0f);
        empty = true;
        HideStats();
    }

    public override void DisplayStats()
    {
        if(!empty)
            InventoryManager.Instance.DisplayStats(item, pos, item.price);
    }


    public void EquipItem()
    {
        if (empty)
        {
            return;
        }

        if (item.type == ItemType.Spell)
        {
            Spell newSpell = new Spell();
            newSpell.name = item.name;
            newSpell.fakeDescription = item.fakeDescription;
            newSpell.id = item.id;
            newSpell.manaCost = item.manaCost;
            newSpell.spellType = item.spellType;
            newSpell.primaryStatValue = item.primaryStatValue;
            newSpell.icon = item.icon;
            newSpell.attackAll = item.attackAll;
            newSpell.activeIcon = ItemManager.Instance.spellRefs.activeIcon[Random.Range(0, ItemManager.Instance.spellRefs.activeIcon.Length)];
            SpellInventory.Instance.AddSpell(newSpell);
            HideStats();
            RemoveItem();
        }
        else if(item.type == ItemType.Armor || item.type == ItemType.Weapon)
        {
            InventoryManager.Instance.ShowEquipPanel(this);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : Slot<Item>
{
    public int price;
    public new void SetSlot(Item newItem)
    {

        icon.color = Color.white;
        item = newItem;
        icon.sprite = item.icon;
        empty = false;
        CalculatePrice();
    }


    void CalculatePrice()
    {
        if (item.type == ItemType.Armor || item.type == ItemType.Weapon)
        {
            price = 0;
            price = item.primaryStatValue * Constants.primaryStatPriceModifier
                + item.secondaryStatValue * Constants.secondaryStatPriceModifier;
        }
        else if (item.type == ItemType.Spell)
        {
            price = 0;
            price = item.manaCost * Constants.primaryStatPriceModifier;
            price += (int)(price * Constants.spellTax);
        }
        else
        {
            //Work out how to price up consumables
            price = 250;
        }

        item.price = price;
    }

    public override void DisplayStats()
    {
        if(!empty)
            InventoryManager.Instance.DisplayStats(item, pos, price);
    }

}

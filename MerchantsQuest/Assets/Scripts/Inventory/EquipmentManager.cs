using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public InventorySlot armorSlot;
    public InventorySlot weaponSlot;

    public void AddToSlot(Item item, ItemType type)
    {
        switch(type)
        {
            case ItemType.Armor:
                if(!armorSlot.empty)
                {
                    Item currItem = armorSlot.GetItem();
                    InventoryManager.Instance.AddItem(currItem);
                    armorSlot.RemoveItem();
                }
                armorSlot.SetSlot(item);
                break;
            case ItemType.Weapon:
                if(!weaponSlot.empty)
                {
                    Item currItem = weaponSlot.GetItem();
                    InventoryManager.Instance.AddItem(currItem);
                    weaponSlot.RemoveItem();
                }
                weaponSlot.SetSlot(item);
                break;
        }
    }

    public void RemoveFromSlot(InventorySlot slot)
    {
        InventoryManager.Instance.AddItem(slot.item);
        slot.RemoveItem();
    }
}

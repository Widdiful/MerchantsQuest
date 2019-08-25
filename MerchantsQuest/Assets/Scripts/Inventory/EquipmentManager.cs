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
                    InventoryManager.Instance.AddItem(currItem.id);
                    armorSlot.RemoveItem();
                }
                armorSlot.SetItem(item.id);
                break;
            case ItemType.Weapon:
                if(!weaponSlot.empty)
                {
                    Item currItem = weaponSlot.GetItem();
                    InventoryManager.Instance.AddItem(currItem.id);
                    weaponSlot.RemoveItem();
                }
                weaponSlot.SetItem(item.id);
                break;
        }
    }

    public void RemoveFromSlot(int id)
    {
        if(id == 0)
        {
            InventoryManager.Instance.AddItem(armorSlot.itemInSlot.id);
            armorSlot.RemoveItem();

        }
        else
        {
            InventoryManager.Instance.AddItem(weaponSlot.itemInSlot.id);
            weaponSlot.RemoveItem();
        }
    }
}

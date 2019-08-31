﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    public InventorySlot armorSlot;
    public InventorySlot weaponSlot;

    Item blankItem;
    public List<Item> items;

    public string charactersName;
    public int player;
    public TMP_Text titleText;

    private void Start()
    {
        if(PartyManager.instance.partyMembers.Count > player)
            charactersName = PartyManager.instance.partyMembers[player].characterName;
        titleText.text = charactersName + " Items";
    }

    public void AddToSlot(Item item, ItemType type)
    {
        switch(type)
        {
            case ItemType.Armor:
                if(!armorSlot.empty)
                {
                    Item currItem = armorSlot.GetItem();
                    items.Remove(armorSlot.GetItem());
                    InventoryManager.Instance.AddItem(currItem);
                    armorSlot.RemoveItem();
                }
                armorSlot.SetSlot(item);
                PartyManager.instance.partyMembers[player].armour = item;
                break;
            case ItemType.Weapon:
                if(!weaponSlot.empty)
                {
                    Item currItem = weaponSlot.GetItem();
                    items.Remove(weaponSlot.GetItem());
                    InventoryManager.Instance.AddItem(currItem);
                    weaponSlot.RemoveItem();
                }
                weaponSlot.SetSlot(item);
                PartyManager.instance.partyMembers[player].weapon = item;
                break;
        }
        items.Add(item);
    }

    public void RemoveFromSlot(InventorySlot slot)
    {
        if (!slot.empty)
        {
            InventoryManager.Instance.AddItem(slot.item);
            if(slot.item.type == ItemType.Armor)
            {
                PartyManager.instance.partyMembers[player].armour = blankItem;
            }
            else
            {
                PartyManager.instance.partyMembers[player].weapon = blankItem;
            }
            InventoryManager.Instance.HideStats();
            items.Remove(slot.item);
            slot.RemoveItem();
        }
    }

    public void RefreshItems()
    {
        if(armorSlot.item.id != 0)
        {
            armorSlot.SetSlot(ItemManager.Instance.GetItemFromID(armorSlot.item.id));
        }
        if(weaponSlot.item.id != 0 )
        {
            weaponSlot.SetSlot(ItemManager.Instance.GetItemFromID(weaponSlot.item.id));
        }
    }
}

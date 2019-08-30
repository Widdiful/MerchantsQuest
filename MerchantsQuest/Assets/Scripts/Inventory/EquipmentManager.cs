using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentManager : MonoBehaviour
{
    public InventorySlot armorSlot;
    public InventorySlot weaponSlot;

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
        items.Add(item);
    }

    public void RemoveFromSlot(InventorySlot slot)
    {
        InventoryManager.Instance.AddItem(slot.item);
        items.Remove(slot.item);
        slot.RemoveItem();
    }
}

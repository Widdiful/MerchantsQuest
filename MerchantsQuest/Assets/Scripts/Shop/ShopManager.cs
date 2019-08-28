using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public ItemType itemForSale;

    public GameObject shopUI;
    public ShopSlot[] shopSlots;
    
    bool shopEnabled = false;

    private void Start()
    {
        FillShopWithItems();
    }

    public void ShowShop(bool active)
    {
        shopEnabled = active;
        shopUI.SetActive(active);


        if (!shopEnabled)
        {
            InventoryManager.Instance.HideStats();
            GameManager.instance.player.canMove = true;
        }
        else
        {
            GameManager.instance.player.canMove = false;
        }
    }

    public bool ShopActive()
    {
        return shopEnabled;
    }


    void FillShopWithItems()
    {
        for(int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].SetSlot(ItemManager.Instance.GenerateSpecificItem(itemForSale));
        }
    }

    public void RefillShop(bool completeRefill)
    {
        if (!completeRefill)
        {
            for (int i = 0; i < shopSlots.Length; i++)
            {
                if (shopSlots[i].empty)
                {
                    shopSlots[i].SetSlot(ItemManager.Instance.GenerateSpecificItem(itemForSale));
                }
            }
        }
        else
        {
            for (int i = 0; i < shopSlots.Length; i++)
            {
                shopSlots[i].SetSlot(ItemManager.Instance.GenerateSpecificItem(itemForSale));
            }
        }
    }

    public void PurchaseItem(ShopSlot slot)
    {
        //Take this away from the  players gold 
        //slot.price 

        if (!slot.empty && PartyManager.instance.gold >= slot.price )
        {
            PartyManager.instance.gold -= slot.price;
            slot.HideStats();
            InventoryManager.Instance.AddItem(slot.item);
            slot.RemoveItem();
        }
    }
}

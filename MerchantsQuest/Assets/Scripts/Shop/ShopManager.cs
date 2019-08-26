using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public ItemType itemForSale;

    public GameObject shopUI;
    public ShopSlot[] shopSlots;

    public KeyCode shopInterfaceKey;

    bool shopEnabled = false;
    private void Start()
    {
        FillShopWithItems();
    }

    public void ShowShop(bool active)
    {
        shopUI.SetActive(active);
    }

    void FillShopWithItems()
    {
        for(int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].SetSlot(ItemManager.Instance.GenerateSpecificItem(itemForSale));
        }
    }

    public void PurchaseItem(ShopSlot slot)
    {
        //Take this away from the  players gold 
        //slot.price 

        InventoryManager.Instance.AddItem(slot.item);
        slot.RemoveItem();
    }
}

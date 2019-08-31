using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleManager : MonoBehaviour
{
    public GameObject shopUI;
    public RecycleSlot[] shopSlots;

    public List<Item> itemsSold;

    RecycleSlot slotThatIsSelling;

    public GameObject confirm;

    bool recycleActive;
    
    public void ShowUI(bool active)
    {
        recycleActive = active;
        shopUI.SetActive(recycleActive);
        if (recycleActive)
        {
            GameManager.instance.player.canMove = false;
        }
        else
        {
            GameManager.instance.player.canMove = true;
            InventoryManager.Instance.HideStats();
        }
        ClearAllSlots();
        FillSlots();
    }

    void FillSlots()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            if(!InventoryManager.Instance.inventorySlots[i].empty)
                shopSlots[i].SetSlot(InventoryManager.Instance.inventorySlots[i].item);
        }
    }

    void ClearAllSlots()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].RemoveItem();
        }
    }

    public void SellItem(RecycleSlot slot)
    {
        if (slot.empty)
            return;

        slotThatIsSelling = slot;
        confirm.SetActive(true);
        Debug.Log("Selling item in " + slot.name);
    }

    public void ConfirmSell()
    {
        confirm.SetActive(false);

        int amountToRecieve = slotThatIsSelling.item.price;
        amountToRecieve = Mathf.RoundToInt((float)amountToRecieve * Constants.sellPercent);
        PartyManager.instance.gold += amountToRecieve;

        CastleManager.instance.lootPile.Add(slotThatIsSelling.item);
        InventoryManager.Instance.RemoveItem(slotThatIsSelling.item.id);

        slotThatIsSelling.RemoveItem();
    }

    public void CancelSell()
    {
        confirm.SetActive(false);
        slotThatIsSelling = null;
    }


    public bool Active()
    {
        return recycleActive;
    }
}

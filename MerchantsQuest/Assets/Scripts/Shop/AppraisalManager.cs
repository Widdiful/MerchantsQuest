using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppraisalManager : MonoBehaviour
{
    public AppraisalSlot[] shopSlots;

    public GameObject shopUI;
    bool activeUI;

    public void ShowUI(bool active)
    {
        activeUI = active;
        shopUI.SetActive(activeUI);
        if(activeUI)
        {
            GameManager.instance.player.canMove = false;
        }
        else
        {
            GameManager.instance.player.canMove = true;
            InventoryManager.Instance.HideStats();
        }
        ClearSlots();
        FillSlots();

    }

    void FillSlots()
    {
        for(int i = 0; i < shopSlots.Length; i++)
        {
            if(!InventoryManager.Instance.inventorySlots[i].empty)
            {
                shopSlots[i].SetSlot(InventoryManager.Instance.inventorySlots[i].item);
            }
        }
    }

    void ClearSlots()
    {
        for(int i = 0; i < shopSlots.Length; i++)
        {
            shopSlots[i].RemoveItem();
        }
    }

    public void AppraiseItem(AppraisalSlot slot)
    {
        if (slot.empty)
            return;

        if (PartyManager.instance.gold >= slot.apraisalCost)
        {
            //Play anim here?
            PartyManager.instance.gold -= slot.apraisalCost;
            slot.item.appraised = true;
            slot.SetSlot(slot.item);
            InventoryManager.Instance.AppraiseItem(slot.item.id);

        }
    }

    public bool Active()
    {
        return activeUI;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleSlot : Slot<Item>
{
    
    public int sellAmount;

    public new void SetSlot(Item newItem)
    {
        base.SetSlot(newItem);
        icon.sprite = item.icon;
        sellAmount = Mathf.RoundToInt(item.price * Constants.sellPercent);
    }
    public override void DisplayStats()
    {
        if(!empty)
        {
            InventoryManager.Instance.DisplayStats(item, pos, sellAmount);
        }
    }
}

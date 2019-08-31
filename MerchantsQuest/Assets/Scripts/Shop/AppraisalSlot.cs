using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppraisalSlot : Slot<Item>
{
    public int apraisalCost;
    public GameObject appraisedOverlay;



    public new void SetSlot(Item newItem)
    {
        icon.color = Color.white;
        item = newItem;
        icon.sprite = item.icon;
        empty = false;
        if (item.appraised)
            appraisedOverlay.SetActive(true);
        CalcApraisal();
    }

    public new void RemoveItem()
    {
        icon.sprite = null;
        icon.color = new Color(.0f, .0f, .0f, .0f);
        empty = true;
        item = blankItem;
        HideStats();
        appraisedOverlay.SetActive(false);
    }

    void CalcApraisal()
    {
        apraisalCost = Mathf.RoundToInt(item.price * Constants.appraisalCost);
    }

    public override void DisplayStats()
    {
        if(!empty)
        {
            InventoryManager.Instance.DisplayStats(item, pos, apraisalCost);
        }
    }
}

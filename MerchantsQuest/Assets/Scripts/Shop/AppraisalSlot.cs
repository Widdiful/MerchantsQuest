using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppraisalSlot : Slot<Item>
{
    public int apraisalCost;


    public new void SetSlot(Item newItem)
    {
        base.SetSlot(newItem);
        icon.sprite = item.icon;
        CalcApraisal();
    }

    void CalcApraisal()
    {
        apraisalCost = Mathf.RoundToInt(item.price * Constants.appraisalCost);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item itemInSlot;
    public bool empty;

    public Image icon;


    private void Awake()
    {
        if(icon.sprite == null)
        {
            icon.color = new Color(0.0f,0.0f,0.0f,0.0f);
        }
    }


    public void SetItem(Item newItem)
    {
        itemInSlot = newItem;
        icon.sprite = itemInSlot.icon;
    }

    public void RemoveItem()
    {
        icon.sprite = null;
        icon.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    }
}


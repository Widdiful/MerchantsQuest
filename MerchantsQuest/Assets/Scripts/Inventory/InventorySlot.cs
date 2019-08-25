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
        empty = true;
    }


    public void SetItem(int id)
    {
        itemInSlot = ItemManager.Instance.GetItemFromID(id);
        empty = false;
        icon.sprite = itemInSlot.icon;
        icon.color = Color.white;
    }

    public void RemoveItem()
    {
        icon.sprite = null;
        icon.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        empty = true;
    }

    public void DisplayStats()
    {
        if(!empty)
            InventoryManager.Instance.DisplayStats(itemInSlot, transform.position);
    }

    public void HideStats()
    {
        if(!empty)
            InventoryManager.Instance.HideStats();
    }
}


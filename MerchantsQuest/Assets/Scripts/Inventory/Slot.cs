using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot<T> : MonoBehaviour
{
    public T item;
    public bool empty;
    public Image icon;

    protected RectTransform pos;

    private void Awake()
    {
        if (icon.sprite == null)
        {
            icon.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        pos = GetComponent<RectTransform>();
    }

    public void SetSlot(T newItem)
    {
        item = newItem;
        empty = false;
        icon.color = Color.white;
    }

    public void RemoveItem()
    {
        icon.sprite = null;
        icon.color = new Color(.0f, .0f, .0f, .0f);
        empty = true;
        HideStats();
    }

    public T GetItem()
    {
        return item;
    }

    public virtual void DisplayStats()
    {

    }

    public void HideStats()
    {
        if (!empty)
        {
            InventoryManager.Instance.HideStats();
        }
    }
}

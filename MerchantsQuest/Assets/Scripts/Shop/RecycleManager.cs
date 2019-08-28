using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleManager : MonoBehaviour
{
    public GameObject shopUI;
    public ShopSlot[] shopSlots;

    public List<Item> itemsSold;

    public void ShowUI(bool active)
    {
        shopUI.SetActive(active);
        if(active)
        {
            GameManager.instance.player.canMove = false;
        }
        else
        {
            GameManager.instance.player.canMove = true;
        }
    }

    public void SellItem(Item item)
    {

    }
}

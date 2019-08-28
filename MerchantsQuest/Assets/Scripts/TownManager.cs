using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    public static TownManager instance;

    public List<ShopManager> shops;

    private void Awake()
    {
        instance = this;
    }

    public void CompleteRefillStock()
    {
        for(int i = 0; i < shops.Count; i++)
        {
            shops[i].RefillShop(true);
        }
    }

    public void RestockEmptySlots()
    {
        for(int i = 0; i < shops.Count; i++)
        {
            shops[i].RefillShop(false);
        }
    }

}

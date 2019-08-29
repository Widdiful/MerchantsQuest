using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public ShopManager armorShop, consumeShop, magicShop, weaponShop;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Duplicate UI in scene");
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }




    
}

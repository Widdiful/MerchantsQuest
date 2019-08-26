using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopManager myShop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                myShop.ShowShop(true);
            }

            if(Input.GetKeyDown(KeyCode.Escape) && myShop.ShopActive())
            {
                myShop.ShowShop(false);
            }
        }
    }


}

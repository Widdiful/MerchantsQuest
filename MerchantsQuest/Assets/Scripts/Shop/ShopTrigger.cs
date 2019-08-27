using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopManager myShop;

    public GameObject shopKeeperSpeech;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("colliding with " + collision.tag);
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

            if(!myShop.ShopActive())
            {
                shopKeeperSpeech.SetActive(true);
            }
            else
            {
                shopKeeperSpeech.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            shopKeeperSpeech.SetActive(false);
        }
    }




}

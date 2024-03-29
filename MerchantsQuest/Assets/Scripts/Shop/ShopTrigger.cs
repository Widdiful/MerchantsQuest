﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public ShopManager myShop;

    public GameObject shopKeeperSpeech;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && !myShop.ShopActive())
            {
                AudioPlayer.Instance.OpenShop();
                myShop.ShowShop(true);
                shopKeeperSpeech.SetActive(false);
                GameManager.instance.player.canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && myShop.ShopActive())
            {
                AudioPlayer.Instance.OpenShop();
                myShop.ShowShop(false);
                GameManager.instance.TogglePause();
                GameManager.instance.player.canMove = true;
            }

            if(!myShop.ShopActive() && !shopKeeperSpeech.activeSelf)
            {
                shopKeeperSpeech.SetActive(true);
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

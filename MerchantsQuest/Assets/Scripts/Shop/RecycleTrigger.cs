using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleTrigger : MonoBehaviour
{
    public RecycleManager myRecycler;

    public GameObject shopKeeperSpeech;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E)&& !myRecycler.Active())
            {
                myRecycler.ShowUI(true);
                shopKeeperSpeech.SetActive(false);
                GameManager.instance.player.canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.E) && myRecycler.Active() )
            {
                myRecycler.ShowUI(false);
                GameManager.instance.TogglePause();
                GameManager.instance.player.canMove = true;
            }

            if (!myRecycler.Active() && !shopKeeperSpeech.activeSelf)
            {
                shopKeeperSpeech.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shopKeeperSpeech.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppraisalTrigger : MonoBehaviour
{
    public AppraisalManager appraiser;

    public GameObject shopKeeperSpeech;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && !appraiser.Active())
            {
                appraiser.ShowUI(true);
                shopKeeperSpeech.SetActive(false);
                GameManager.instance.player.canMove = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && appraiser.Active())
            {
                appraiser.ShowUI(false);
                GameManager.instance.TogglePause();
                GameManager.instance.player.canMove = true;
            }

            if (!appraiser.Active() && !shopKeeperSpeech.activeSelf)
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

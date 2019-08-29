using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitions : MonoBehaviour
{
    public GameObject nextMap, thisMap;
    public Transform entrancePoint;
    public dungeonGeneration dungeon;
    public bool encountersAllowed, leadsToDungeon, leadsToWorldMap;

    public void Transition(Transform player) {
        
        GameManager.instance.encountersAllowed = encountersAllowed;
        if (leadsToDungeon) {
            CameraManager.instance.SetPlayerCamActive();
            dungeon.startDungeon();
        }
        else {
            if(leadsToWorldMap)
            {
                CameraManager.instance.SetPlayerCamActive();
            }
            else
            {
                CameraManager.instance.SetTownCamActive();
            }
            nextMap.SetActive(true);
            player.position = entrancePoint.position;
            GameManager.instance.player.lerping = false;
            thisMap.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Transition(collision.transform);
        }
    }
}

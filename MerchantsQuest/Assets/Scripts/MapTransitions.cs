using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitions : MonoBehaviour
{
    public GameObject nextMap, thisMap;
    public Transform entrancePoint;
    public dungeonGeneration dungeon;
    public bool encountersAllowed, leadsToDungeon, disableCamera;

    public void Transition(Transform player) {
        player.gameObject.GetComponent<PlayerController>().combatCamera.enabled = !disableCamera;
        GameManager.instance.encountersAllowed = encountersAllowed;
        if (leadsToDungeon) {
            dungeon.generateMap();
        }
        else {
            nextMap.SetActive(true);
            player.position = entrancePoint.position;
            thisMap.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Transition(collision.transform);
        }
    }
}

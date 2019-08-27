using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitions : MonoBehaviour
{
    public GameObject nextMap, thisMap;
    public Transform entrancePoint;
    public bool encountersAllowed;

    public void Transition(Transform player) {
        GameManager.instance.encountersAllowed = encountersAllowed;
        nextMap.SetActive(true);
        player.position = entrancePoint.position;
        thisMap.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Transition(collision.transform);
        }
    }
}

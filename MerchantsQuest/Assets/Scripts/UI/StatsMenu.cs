using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsMenu : MonoBehaviour
{
    public GameObject statsPanePrefab;
    public Transform container;

    public void InitialiseMenu() {
        foreach (Transform transform in container) {
            Destroy(transform.gameObject);
        }

        foreach (PlayerStats player in PartyManager.instance.partyMembers) {
            StatsMenuItem item = Instantiate(statsPanePrefab, container).GetComponent<StatsMenuItem>();
            item.SetValues(player);
        }
    }
}

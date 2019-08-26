﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform listContainer;
    public CommandMenu commandMenu;

    public void InitialiseTargetList() {
        InitialiseTargetList(false);
    }

    public void InitialiseTargetList(bool targetTeam) {
        foreach(Transform transform in listContainer) {
            Destroy(transform.gameObject);
        }

        if (!targetTeam) {
            for (int i = 0; i < CombatManager.instance.enemyTeam.Count; i++) {
                TargetingButton button = Instantiate(buttonPrefab, listContainer).GetComponent<TargetingButton>();
                button.Initialise(i, CombatManager.instance.enemyTeam[i].characterName, commandMenu, targetTeam);
            }
        }
        else {
            for (int i = 0; i < CombatManager.instance.allAllies.Count; i++) {
                TargetingButton button = Instantiate(buttonPrefab, listContainer).GetComponent<TargetingButton>();
                button.Initialise(i, CombatManager.instance.allAllies[i].characterName, commandMenu, targetTeam);
            }
        }
    }
}

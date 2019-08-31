using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitions : MonoBehaviour
{
    public GameObject nextMap, thisMap;
    public Transform entrancePoint;
    public dungeonGeneration dungeon;
    public bool encountersAllowed, leadsToDungeon, leadsToWorldMap, fullyHealParty;

    public void Transition(Transform player) {
        StartCoroutine(StartTransition(player));
    }

    IEnumerator StartTransition(Transform player) {
        
        GameManager.instance.encountersAllowed = encountersAllowed;
        if (fullyHealParty) {
            foreach(PlayerStats stats in PartyManager.instance.partyMembers) {
                if (!stats.isDead) {
                    stats.currentHP = stats.maxHP;
                    stats.currentMP = stats.maxMP;
                }
            }
        }

        if (leadsToDungeon) {
            CameraManager.instance.SetPlayerCamActive();
            dungeon.startDungeon();
        }
        else {
            while (!GameManager.instance.transition.textureHidden) {
                yield return null;
            }
            GameManager.instance.transition.BeginShow();
            GameManager.instance.player.canMove = false;
            while (!GameManager.instance.transition.textureShown) {
                yield return null;
            }
            GameManager.instance.transition.BeginHide();
            GameManager.instance.player.canMove = true;

            if (leadsToWorldMap)
            {
                CameraManager.instance.SetPlayerCamActive();
                AudioManager.Instance.TransitionToOverworldBGM();
            }
            else
            {
                CameraManager.instance.SetTownCamActive();
                AudioManager.Instance.TransitionToTownBGM();
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

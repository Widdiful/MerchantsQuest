using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject combatRoot, overworldRoot;
    public CombatManager combatManager;
    public Vector2Int minMaxEncounterRate;
    public float transitionTime;
    public bool encountersAllowed;
    private int stepsUntilEncounter;

    public static GameManager instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        stepsUntilEncounter = Random.Range(minMaxEncounterRate.x, minMaxEncounterRate.y);
    }

    public void StartCombat() {
        combatRoot.SetActive(true);
        overworldRoot.SetActive(false);
        combatManager.StartCombat();
    }

    public void EndCombat() {
        combatRoot.SetActive(false);
        overworldRoot.SetActive(true);
    }

    public void Step(PlayerController player) {
        if (encountersAllowed) {
            stepsUntilEncounter--;
            if (stepsUntilEncounter <= 0) {
                stepsUntilEncounter = Random.Range(minMaxEncounterRate.x, minMaxEncounterRate.y);
                StartCoroutine(CombatTransition(player));
            }
        }
    }

    IEnumerator CombatTransition(PlayerController player) {
        player.canMove = false;
        yield return new WaitForSeconds(transitionTime);
        StartCombat();
        player.canMove = true;
    }
}

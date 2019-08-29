using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject combatRoot, overworldRoot, worldMapRoot;
    public CombatManager combatManager;
    public Vector2Int minMaxEncounterRate;
    public Vector2 dungeonExitPoint;
    public dungeonGeneration dungeon;
    public PlayerController player;
    public float transitionTime;
    public bool encountersAllowed;
    public bool inCombat;
    public GameObject pauseMenu;
    public GameObject evacButton;
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
        inCombat = true;
    }

    public void EndCombat() {
        combatRoot.SetActive(false);
        overworldRoot.SetActive(true);
        inCombat = false;
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

    private void Update() {
        if (Input.GetKeyDown("escape")) {
            TogglePause();
        }
    }


    IEnumerator CombatTransition(PlayerController player) {
        player.canMove = false;
        yield return new WaitForSeconds(transitionTime);
        StartCombat();
        player.canMove = true;
    }

    public void ExitDungeon() {
        if (dungeon.isInDungeon) {
            worldMapRoot.SetActive(true);
            TogglePause();
            dungeon.shutDungeonDown();
            player.transform.position = dungeonExitPoint;
        }
    }

    public void TogglePause() {
        evacButton.SetActive(dungeon.isInDungeon);
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }
}

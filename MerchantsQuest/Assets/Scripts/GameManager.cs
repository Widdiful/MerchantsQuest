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
    public Canvas victoryCanvas, gameOverCanvas;
    private int stepsUntilEncounter;

    public TransitionBoi transition;

    public static GameManager instance;



    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        transition.BeginHide();
        stepsUntilEncounter = Random.Range(minMaxEncounterRate.x, minMaxEncounterRate.y);
    }

    public void StartCombat() {
        StartCombat(null);
    }

    public void StartCombat(List<EnemyStats> enemies) {
        combatRoot.SetActive(true);
        overworldRoot.SetActive(false);
        combatManager.StartCombat(enemies);
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
        while (!transition.textureHidden) {
            yield return null;
        }
        transition.BeginShow();
        while (!transition.textureShown) {
            yield return null;
        }
        transition.BeginHide();
        StartCombat();
        player.canMove = true;
    }

    public void ExitDungeon() {
        if (dungeon.isInDungeon) {
            worldMapRoot.SetActive(true);
            TogglePause();
            dungeon.shutDungeonDown();
        }
    }

    public void TogglePause() {
        evacButton.SetActive(dungeon.isInDungeon);
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
    }

    public void GameOver() {
        player.canMove = false;
        gameOverCanvas.enabled = true;
    }

    public void CompleteGame() {
        player.canMove = false;
        victoryCanvas.enabled = true;
    }
}

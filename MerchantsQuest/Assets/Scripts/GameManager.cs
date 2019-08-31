using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public Canvas victoryCanvas, gameOverCanvas, spellCanvas, targetingCanvas, statsCanvas;
    public SpellMenu spellMenu;
    public StatsMenu statsMenu;
    private int stepsUntilEncounter;
    private bool gameOver;

    public TransitionBoi transition;

    public static GameManager instance;



    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        stepsUntilEncounter = Random.Range(minMaxEncounterRate.x, minMaxEncounterRate.y);
    }

    public void StartCombat() {
        StartCombat(null);
    }

    public void StartCombat(List<EnemyStats> enemies) {
        combatRoot.SetActive(true);
        overworldRoot.SetActive(false);
        AudioManager.Instance.TransitionToCombatBGM();
        combatManager.StartCombat(enemies);
        inCombat = true;
    }

    public void StartCombat(List<EnemyStats> enemies, Transform escapePoint, bool bossEncounter) {
        combatRoot.SetActive(true);
        overworldRoot.SetActive(false);
        if (bossEncounter)
            AudioManager.Instance.TransitionToBossBGM();
        else
            AudioManager.Instance.TransitionToCombatBGM();
        combatManager.StartCombat(enemies, escapePoint, bossEncounter);
        inCombat = true;
    }

    public void EndCombat() {
        combatRoot.SetActive(false);
        overworldRoot.SetActive(true);
        AudioManager.Instance.TransitionToDungeonBGM();
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

        if (gameOver && Input.anyKeyDown) {
            SceneManager.LoadScene(0);
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
            AudioManager.Instance.TransitionToOverworldBGM();
            TogglePause();
            dungeon.shutDungeonDown();
        }
    }

    public void TogglePause() {
        evacButton.SetActive(dungeon.isInDungeon);
        bool val = !pauseMenu.activeInHierarchy;
        pauseMenu.SetActive(val);
        spellCanvas.enabled = false;
        targetingCanvas.enabled = false;
        statsCanvas.enabled = false;
    }

    public void GameOver() {
        player.canMove = false;
        gameOverCanvas.enabled = true;
        gameOver = true;
    }

    public void CompleteGame() {
        player.canMove = false;
        victoryCanvas.enabled = true;
        gameOver = true;
    }

    public void OpenOverworldSpellMenu() {
        statsCanvas.enabled = false;
        targetingCanvas.enabled = false;
        spellMenu.InitialiseTargetList();
        spellCanvas.enabled = true;
    }

    public void OpenStatsMenu() {
        targetingCanvas.enabled = false;
        spellCanvas.enabled = false;
        statsCanvas.enabled = true;
        statsMenu.InitialiseMenu();
    }
}

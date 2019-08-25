using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    public static CombatManager instance;
    public List<PlayerStats> playerTeam = new List<PlayerStats>();
    public List<EnemyStats> enemyTeam = new List<EnemyStats>();
    public List<StatsBase> turnOrder = new List<StatsBase>();
    public Canvas commandCanvas, targetingCanvas, messageCanvas;
    public StatsBase currentActor;
    public float timeToWait;
    public Text messageText;
    private int turnIndex;
    private bool battleEnded;

    private void Awake() {
        instance = this;

        playerTeam = PartyManager.instance.partyMembers;

        foreach(StatsBase player in playerTeam) {
            turnOrder.Add(player);
        }

        foreach (StatsBase enemy in enemyTeam) {
            turnOrder.Add(enemy);
        }

        // sort by agility stat

        StartCoroutine(StartBattle());
    }

    public void NextTurn() {
        DisableCommandCanvas();
        messageText.text = "";
        if (!battleEnded) {
            if (enemyTeam.Count > 0 && playerTeam.Count > 0) {

                if (!turnOrder[turnIndex].isDead) {
                    currentActor = turnOrder[turnIndex];
                    currentActor.GetCommand();
                }

                turnIndex = (turnIndex + 1) % (playerTeam.Count + enemyTeam.Count);

            }
            else if (enemyTeam.Count <= 0) {
                messageText.text += ("Victory!");
                battleEnded = true;
            }
            else if (playerTeam.Count <= 0) {
                messageText.text += ("You lose!");
                battleEnded = true;
            }
        }
    }

    public void Attack(StatsBase attacker, StatsBase target, int damage, bool ignoreDefence) {
        int damageTaken = target.TakeDamage(damage, ignoreDefence);

        if (damageTaken > 0) {
            if (ignoreDefence) messageText.text += "Critical hit! \n";
            messageText.text += (attacker.characterName + " attacked " + target.characterName + " for " + damageTaken + " damage.");
        }
        else {
            messageText.text += attacker.characterName + " missed!";
        }

        if (target.isDead) {
            messageText.text += ("\n" + target.characterName + " died!");
        }

        StartCoroutine(NextTurnWait());
    }

    public void Defend(StatsBase defender) {
        messageText.text += defender.characterName + " defends.";

        StartCoroutine(NextTurnWait());
    }

    public void EnableCommandCanvas() {
        commandCanvas.enabled = true;
        targetingCanvas.enabled = false;
        messageCanvas.enabled = false;
    }

    public void DisableCommandCanvas() {
        commandCanvas.enabled = false;
        targetingCanvas.enabled = false;
        messageCanvas.enabled = true;
    }

    IEnumerator StartBattle() {
        DisableCommandCanvas();
        messageText.text = "";

        for (int i = 0; i < enemyTeam.Count; i++) {
            messageText.text += enemyTeam[i].characterName;
            if (i < enemyTeam.Count - 2) {
                messageText.text += ", ";
            }
            else if (i == enemyTeam.Count - 2) {
                messageText.text += " and ";
            }
        }
        messageText.text += " appear!";

        yield return new WaitForSeconds(timeToWait);
        NextTurn();
    }

    IEnumerator NextTurnWait() {
        DisableCommandCanvas();
        int waitMultiplier = Regex.Matches(messageText.text, "\n").Count + 1;
        yield return new WaitForSeconds(timeToWait * waitMultiplier);
        NextTurn();
    }
}

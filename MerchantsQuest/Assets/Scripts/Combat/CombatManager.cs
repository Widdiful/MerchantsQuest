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
    public List<EnemyStats> possibleEnemies = new List<EnemyStats>();
    public int maxEnemies;
    public Canvas commandCanvas, targetingCanvas, messageCanvas;
    public StatsBase currentActor;
    public float timeToWait;
    public Text messageText;
    private int turnIndex = 0;
    private bool battleEnded;
    private Dictionary<string, int> enemyDict = new Dictionary<string, int>();

    private void Awake() {
        instance = this;

        playerTeam = PartyManager.instance.partyMembers;

        int enemyCount = Random.Range(1, maxEnemies + 1);
        for(int i = 0; i < enemyCount; i++) {
            EnemyStats temp = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count)]);
            enemyTeam.Add(temp);
            if (enemyDict.ContainsKey(temp.characterName)) {
                enemyDict[temp.characterName]++;
            }
            else {
                enemyDict[temp.characterName] = 1;
            }
        }

        Dictionary<string, int> counterDict = new Dictionary<string, int>();
        foreach(EnemyStats enemy in enemyTeam) {
            if (enemyDict[enemy.characterName] > 1) {
                if (counterDict.ContainsKey(enemy.characterName)) {
                    counterDict[enemy.characterName]++;
                }
                else {
                    counterDict[enemy.characterName] = 1;
                }
                enemy.characterName += " " + System.Convert.ToChar(counterDict[enemy.characterName] + 64);
            }
        }

        foreach (StatsBase player in playerTeam) {
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
            if (enemyTeam.Count == 1) {
                messageText.text += "A ";
            }
            messageText.text += enemyTeam[i].characterName;
            if (i < enemyTeam.Count - 2) {
                messageText.text += ", ";
            }
            else if (i == enemyTeam.Count - 2) {
                messageText.text += " and ";
            }
        }
        if (enemyTeam.Count > 1) {
            messageText.text += " appear!";
        }
        else {
            messageText.text += " appears!";
        }

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

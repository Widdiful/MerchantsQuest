using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public static CombatManager instance;
    public List<PlayerStats> playerTeam = new List<PlayerStats>();
    public List<EnemyStats> enemyTeam = new List<EnemyStats>();
    public List<StatsBase> turnOrder = new List<StatsBase>();
    public Canvas commandCanvas;
    public StatsBase currentActor;
    private int turnIndex;
    private bool battleEnded;

    private void Awake() {
        instance = this;

        foreach(StatsBase player in playerTeam) {
            turnOrder.Add(player);
        }

        foreach (StatsBase enemy in enemyTeam) {
            turnOrder.Add(enemy);
        }

        // sort by agility stat

        NextTurn();
    }

    public void NextTurn() {
        DisableCommandCanvas();
        if (!battleEnded) {
            if (enemyTeam.Count > 0 && playerTeam.Count > 0) {

                turnIndex = (turnIndex + 1) % (playerTeam.Count + enemyTeam.Count);

                if (!turnOrder[turnIndex].isDead) {
                    currentActor = turnOrder[turnIndex];
                    currentActor.GetCommand();
                }

            }
            else if (enemyTeam.Count <= 0) {
                print("victory!");
                battleEnded = true;
            }
            else if (playerTeam.Count <= 0) {
                print("failure!");
                battleEnded = true;
            }
        }
    }

    public void Attack(StatsBase attacker, StatsBase target, int damage, bool ignoreDefence) {
        int damageTaken = target.TakeDamage(damage, ignoreDefence);

        print(attacker.characterName + " attacked " + target.characterName + " for " + damageTaken);

        if (target.isDead) {
            print(target.characterName + " died!");
        }

        NextTurn();
    }

    public void EnableCommandCanvas() {
        commandCanvas.enabled = true;
    }

    public void DisableCommandCanvas() {
        commandCanvas.enabled = false;
    }
}

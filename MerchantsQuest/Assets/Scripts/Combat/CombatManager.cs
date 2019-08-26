using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    public static CombatManager instance;
    public List<PlayerStats> playerTeam = new List<PlayerStats>();
    public List<EnemyStats> enemyTeam, allEnemies = new List<EnemyStats>();
    public List<StatsBase> turnOrder = new List<StatsBase>();
    public List<EnemyStats> possibleEnemies = new List<EnemyStats>();
    public AnimationCurve numberOfEnemies;
    public Canvas commandCanvas, targetingCanvas, messageCanvas;
    public Transform statsPanel;
    public GameObject statsPrefab;
    public StatsBase currentActor;
    public float timeToWait;
    public Text messageText;
    public EnemySpriteManager spriteManager;
    private int turnIndex = 0;
    private bool battleEnded;
    private Dictionary<StatsBase, CharacterPanel> statsPanels = new Dictionary<StatsBase, CharacterPanel>();
    public AnimatedBackground background;
    public int expEarned, goldEarned;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartCombat() {
        StartCoroutine(StartBattle());
        background.Randomise();
    }

    public void NextTurn() {
        DisableCommandCanvas();
        messageText.text = "";

        if (!battleEnded) {
            if (enemyTeam.Count > 0 && playerTeam.Count > 0) {

                turnIndex = (turnIndex + 1) % (playerTeam.Count + enemyTeam.Count);

                if (!turnOrder[turnIndex].isDead) {
                    currentActor = turnOrder[turnIndex];

                    if (statsPanels.Keys.Contains<StatsBase>(currentActor)) {
                        statsPanels[currentActor].ToggleIndicator();
                    }

                    currentActor.GetCommand();
                }
                else {
                    NextTurn();
                }

            }
            else if (enemyTeam.Count <= 0) {
                messageText.text += ("Victory!");
                battleEnded = true;
                StartCoroutine(NextTurnWait());
            }
            else if (playerTeam.Count <= 0) {
                messageText.text += ("You lose!");
                battleEnded = true;
                StartCoroutine(NextTurnWait());
            }
        }
        else {
            StartCoroutine(EndBattle());
        }
    }

    public void Attack(StatsBase attacker, StatsBase target, int damage, bool ignoreDefence) {
        int damageTaken = target.TakeDamage(damage, ignoreDefence);

        if (damageTaken > 0) {
            if (ignoreDefence) messageText.text += "Critical hit! \n";
            messageText.text += string.Format("{0} attacked {1} for {2} damage.", attacker.characterName, target.characterName, damageTaken);
        }
        else {
            messageText.text += string.Format("{0} missed!", attacker.characterName);
        }

        if (target.isDead) {
            messageText.text += string.Format("\n {0} died!", target.characterName);
        }

        StartCoroutine(NextTurnWait());
    }

    public void Defend(StatsBase defender) {
        messageText.text += string.Format("{0} defends.", defender.characterName);

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

    public void UpdateAllStats() {
        foreach(KeyValuePair<StatsBase, CharacterPanel> pair in statsPanels) {
            pair.Value.UpdateStats();
        }
    }

    IEnumerator StartBattle() {
        DisableCommandCanvas();

        // Reset values
        battleEnded = false;
        playerTeam.Clear();
        enemyTeam.Clear();
        allEnemies.Clear();
        turnOrder.Clear();
        statsPanels.Clear();
        turnIndex = -1;
        foreach(Transform child in statsPanel) {
            Destroy(child.gameObject);
        }

        // Initialise party members
        foreach(PlayerStats player in PartyManager.instance.partyMembers) {
            PlayerStats temp = Instantiate(player);
            temp.InitialiseCharacter();

            CharacterPanel panel = Instantiate(statsPrefab, statsPanel).GetComponent<CharacterPanel>();
            panel.UpdateStats(temp);
            statsPanels[temp] = panel;

            if (!player.isDead) {
                playerTeam.Add(temp);
            }
        }
        foreach (StatsBase player in playerTeam) {
            turnOrder.Add(player);
        }

        // Initialise enemies
        int enemyCount = (int)numberOfEnemies.Evaluate(Random.Range(0f, 1f));
        Dictionary<string, int> enemyDict = new Dictionary<string, int>();
        for (int i = 0; i < enemyCount; i++) {
            EnemyStats temp = Instantiate(possibleEnemies[Random.Range(0, possibleEnemies.Count)]);
            temp.InitialiseCharacter();
            enemyTeam.Add(temp);
            allEnemies.Add(temp);
            if (enemyDict.ContainsKey(temp.characterName)) {
                enemyDict[temp.characterName]++;
            }
            else {
                enemyDict[temp.characterName] = 1;
            }
        }

        // Name enemies properly
        Dictionary<string, int> counterDict = new Dictionary<string, int>();
        foreach (EnemyStats enemy in enemyTeam) {
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

        foreach (StatsBase enemy in enemyTeam) {
            turnOrder.Add(enemy);
        }

        // TODO: sort turnOrder by agility stat

        // Generate first message
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

        UpdateAllStats();
        spriteManager.SetSprites(enemyTeam);

        yield return new WaitForSeconds(timeToWait);
        NextTurn();
    }

    IEnumerator EndBattle() {
        PartyManager.instance.gold += goldEarned;
        messageText.text = string.Format("The party has earned {0} experience points and {1} gold.", expEarned, goldEarned);
        yield return new WaitForSeconds(timeToWait);

        for (int i = 0; i < PartyManager.instance.partyMembers.Count; i++) {
            StatsBase player = statsPanels.Keys.ElementAt<StatsBase>(i);

            if (!player.isDead) {
                player.totalXP += expEarned;
                while (player.totalXP >= player.targetXP) {
                    player.level++;
                    player.targetXP += (player.level * 100);
                    messageText.text = string.Format("{0}'s level has increased to {1}!", player.characterName, player.level);
                    yield return new WaitForSeconds(timeToWait);
                }
            }


            PartyManager.instance.partyMembers[i].currentHP = player.currentHP;
            PartyManager.instance.partyMembers[i].currentMP = player.currentMP;
            PartyManager.instance.partyMembers[i].level = player.level;
            PartyManager.instance.partyMembers[i].totalXP = player.totalXP;
            PartyManager.instance.partyMembers[i].targetXP = player.targetXP;
            PartyManager.instance.partyMembers[i].isDead = player.isDead;
        }

        expEarned = 0;
        goldEarned = 0;

        yield return new WaitForSeconds(timeToWait * 2);
        GameManager.instance.EndCombat();
    }

    IEnumerator NextTurnWait() {
        DisableCommandCanvas();
        UpdateAllStats();
        spriteManager.UpdateSprites(allEnemies);
        int waitMultiplier = Regex.Matches(messageText.text, "\n").Count + 1;
        yield return new WaitForSeconds(timeToWait * waitMultiplier);
        NextTurn();
    }

    public void ToggleIndicator() {
        if (currentActor) {
            statsPanels[currentActor].ToggleIndicator();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{

    public static CombatManager instance;
    public List<PlayerStats> playerTeam, allAllies = new List<PlayerStats>();
    public List<EnemyStats> enemyTeam, allEnemies = new List<EnemyStats>();
    public List<StatsBase> turnOrder = new List<StatsBase>();
    public List<EnemyStats> possibleEnemies = new List<EnemyStats>();
    public AnimationCurve numberOfEnemies;
    public Canvas commandCanvas, targetingCanvas, messageCanvas, spellCanvas, itemCanvas;
    public Transform statsPanel;
    public GameObject statsPrefab;
    public StatsBase currentActor;
    public float timeToWait;
    public TextMeshProUGUI messageText;
    public EnemySpriteManager spriteManager;
    private int turnIndex = 0;
    private bool battleEnded;
    private Dictionary<StatsBase, CharacterPanel> statsPanels = new Dictionary<StatsBase, CharacterPanel>();
    public AnimatedBackground background;
    public int expEarned, goldEarned;
    public dungeonGeneration dungeon;
    public Transform escapePoint;
    private bool isBossEncounter;
    private bool gameOver;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() {
        if (!dungeon)
            dungeon = GameObject.FindObjectOfType<dungeonGeneration>();
    }

    public void StartCombat() {
        StartCoroutine(StartBattle(null));
        background.Randomise();
        escapePoint = null;
        isBossEncounter = false;
    }

    public void StartCombat(List<EnemyStats> enemies) {
        StartCoroutine(StartBattle(enemies));
        background.Randomise();
        escapePoint = null;
        isBossEncounter = false;
    }

    public void StartCombat(List<EnemyStats> enemies, Transform fleePoint, bool bossEncounter) {
        StartCoroutine(StartBattle(enemies));
        background.Randomise();
        escapePoint = fleePoint;
        isBossEncounter = bossEncounter;
    }

    public void NextTurn() {
        DisableCommandCanvas();

        if (!battleEnded) {
            messageText.text = "";

            if (enemyTeam.Count > 0 && playerTeam.Count > 0) {

                turnIndex = (turnIndex + 1) % (allAllies.Count + allEnemies.Count);

                if (turnIndex < turnOrder.Count && !turnOrder[turnIndex].isDead) {
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
                messageText.text += ("Victory!\n");
                AudioPlayer.Instance.VictoryFanfare();
                battleEnded = true;
                StartCoroutine(NextTurnWait());
            }
            else if (playerTeam.Count <= 0) {
                messageText.text += ("The party are wiped out!\n");
                AudioManager.Instance.SilenceAllBGM();
                AudioPlayer.Instance.GameOver();
                battleEnded = true;
                gameOver = true;
                StartCoroutine(NextTurnWait());
            }
        }
        else {
            StartCoroutine(EndBattle(true));
        }
    }

    public void Attack(StatsBase attacker, StatsBase target, int damage, bool ignoreDefence, bool lifesteal, bool showAnimation) {
        Attack(attacker, target, damage, ignoreDefence, lifesteal, showAnimation, false);
    }

    public void Attack(StatsBase attacker, StatsBase target, int damage, bool ignoreDefence, bool lifesteal, bool showAnimation, bool dontAdvanceAfterAttack) {
        int damageTaken = target.TakeDamage(damage, ignoreDefence);

        if (damageTaken > 0) {
            if (ignoreDefence) messageText.text += "Critical hit!\n";
            if (!lifesteal) {
                messageText.text += string.Format("{0} takes {1} damage.", target.characterName, damageTaken);
                if (showAnimation) {
                    AudioPlayer.Instance.TakeDamage();
                }
            }
            else {
                attacker.Heal(damageTaken);
                messageText.text += string.Format("Absorbs {0} HP from {1}!", damageTaken, target.characterName);
                AudioPlayer.Instance.HealSpell();
            }

            if (target.isEnemy && showAnimation) {
                spriteManager.PlayMeleeEffect(allEnemies.IndexOf((EnemyStats)target));
                if (target.currentHP > 0) {
                    spriteManager.FlickerSprite(allEnemies.IndexOf((EnemyStats)target));
                }
            }
        }
        else {
            messageText.text += string.Format("{0} blocks the attack.", target.characterName);
            AudioPlayer.Instance.Block();
        }

        if (target.isDead) {
            messageText.text += string.Format("\n{0} dies!", target.characterName);
        }

        if (!dontAdvanceAfterAttack) {
            StartCoroutine(NextTurnWait());
        }
    }

    public void Heal(StatsBase healer, StatsBase target, int amount) {
        Heal(healer, target, amount, false);
    }

    public void Heal(StatsBase healer, StatsBase target, int amount, bool dontContinueAfterAttack) {
        target.Heal(amount);

        messageText.text += string.Format("{0} heals {1} health.", target.characterName, amount);
        AudioPlayer.Instance.HealSpell();

        if (!dontContinueAfterAttack) {
            StartCoroutine(NextTurnWait());
        }
    }

    public void StartAttack(StatsBase attacker, StatsBase target, int damage, bool ignoreDefence) {
        DisableCommandCanvas();
        StartCoroutine(AttackDelay(attacker, target, damage, ignoreDefence));
    }

    IEnumerator AttackDelay(StatsBase attacker, StatsBase target, int damage, bool ignoreDefence) {
        messageText.text += string.Format("{0} attacks!\n", attacker.characterName);
        AudioPlayer.Instance.Attack();
        yield return new WaitForSeconds(timeToWait);
        Attack(attacker, target, damage, ignoreDefence, false, true);
    }

    public void SpellAttack(StatsBase attacker, StatsBase target, Spell spell) {
        DisableCommandCanvas();
        StartCoroutine(SpellDelay(attacker, target, spell));
    }

    public void SpellAttackAll(StatsBase attacker, Spell spell) {
        DisableCommandCanvas();
        StartCoroutine(SpellDelayAll(attacker, spell));
    }

    IEnumerator SpellDelay(StatsBase attacker, StatsBase target, Spell spell) {
        messageText.text += string.Format("{0} casts {1}!\n", attacker.characterName, spell.name);
        AudioPlayer.Instance.CastSpell();
        yield return new WaitForSeconds(timeToWait);
        if (attacker.currentMP >= spell.manaCost) {
            attacker.currentMP -= spell.manaCost;
            UseSpell(attacker, target, spell, false);
        }
        else {
            messageText.text += "Not enough MP!";
            StartCoroutine(NextTurnWait());
        }
    }

    IEnumerator SpellDelayAll(StatsBase attacker, Spell spell) {
        messageText.text += string.Format("{0} casts {1}!\n", attacker.characterName, spell.name);
        AudioPlayer.Instance.CastSpell();
        if (attacker.currentMP >= spell.manaCost) {
            List<StatsBase> targets = new List<StatsBase>();
            if ((!attacker.isEnemy && spell.spellType != SpellType.Heal)|| (attacker.isEnemy && spell.spellType == SpellType.Heal)) {
                foreach(StatsBase stats in enemyTeam) {
                    targets.Add(stats);
                }
            }
            else {
                foreach (StatsBase stats in playerTeam) {
                    targets.Add(stats);
                }
            }
            foreach(StatsBase target in targets) {
                yield return new WaitForSeconds(timeToWait);
                messageText.text = "";
                attacker.currentMP -= spell.manaCost;
                UseSpell(attacker, target, spell, true);
                spriteManager.UpdateSprites(allEnemies);
                UpdateAllStats();
            }

            StartCoroutine(NextTurnWait());
        }
        else {
            yield return new WaitForSeconds(timeToWait);
            messageText.text += "Not enough MP!";
            StartCoroutine(NextTurnWait());
        }
    }

    public void UseItem(StatsBase attacker, StatsBase target, Item item) {
        DisableCommandCanvas();
        StartCoroutine(ItemDelay(attacker, target, item));
    }

    IEnumerator ItemDelay(StatsBase attacker, StatsBase target, Item item) {
        messageText.text += string.Format("{0} uses {1}!\n", attacker.characterName, item.name);
        yield return new WaitForSeconds(timeToWait);
        switch (item.spellType) {
            case SpellType.None:
                messageText.text += "You can't use this!";
                StartCoroutine(NextTurnWait());
                break;
            case SpellType.Damage:
                Attack(attacker, target, item.primaryStatValue, true, false, false);

                if (target.isEnemy) {
                    spriteManager.PlayMagicEffect(allEnemies.IndexOf((EnemyStats)target));
                    if (target.currentHP > 0) {
                        spriteManager.FlickerSprite(allEnemies.IndexOf((EnemyStats)target));
                    }
                }
                break;
            case SpellType.Physical:
                Attack(attacker, target, item.primaryStatValue, true, false, false);
                break;
            case SpellType.Heal:
                Heal(attacker, target, item.primaryStatValue);
                break;
        }
        if (item.type == ItemType.Consumable) {
            // Decrease quantity
            InventoryManager.Instance.RemoveItem(item.id);
        }
    }

    private void UseSpell(StatsBase attacker, StatsBase target, Spell spell, bool dontContinueAfterAttack) {
        switch (spell.spellType) {
            case SpellType.None:
                messageText.text += "Nothing happens!";
                StartCoroutine(NextTurnWait());
                break;
            case SpellType.Damage:
                Attack(attacker, target, spell.primaryStatValue + attacker.currentINT, false, spell.lifesteal, false, dontContinueAfterAttack);

                AudioPlayer.Instance.DamageSpell();

                if (target.isEnemy) {
                    spriteManager.PlayMagicEffect(allEnemies.IndexOf((EnemyStats)target));
                    if (target.currentHP > 0) {
                        spriteManager.FlickerSprite(allEnemies.IndexOf((EnemyStats)target));
                    }
                }
                break;
            case SpellType.Physical:
                Attack(attacker, target, spell.primaryStatValue + attacker.currentATK, false, spell.lifesteal, true, dontContinueAfterAttack);
                break;
            case SpellType.Heal:
                if (!target.isDead) {
                    Heal(attacker, target, spell.primaryStatValue + attacker.currentINT, dontContinueAfterAttack);
                }
                else {
                    messageText.text += string.Format("But {0} is dead!", target.characterName);
                }
                break;
        }
        if (spell.id != 0 && !spell.appraised)
            SpellInventory.Instance.AppraiseSpell(spell.id);
    }

    public void Defend(StatsBase defender) {
        messageText.text += string.Format("{0} defends.", defender.characterName);

        StartCoroutine(NextTurnWait());
    }

    public void Flee() {
        DisableCommandCanvas();
        messageText.text = string.Format("{0} flees...\n", currentActor.characterName);
        AudioPlayer.Instance.Flee();
        StartCoroutine(FleeDelay());
    }

    IEnumerator FleeDelay() {
        yield return new WaitForSeconds(timeToWait);
        if (Random.Range(0, 2) == 0) {
            StartCoroutine(EndBattle(false));
        }
        else {
            messageText.text += "But fails to escape!";
            StartCoroutine(NextTurnWait());
        }
    }

    public void EnableCommandCanvas() {
        commandCanvas.enabled = true;
        targetingCanvas.enabled = false;
        messageCanvas.enabled = false;
        spellCanvas.enabled = false;
        itemCanvas.enabled = false;
    }

    public void DisableCommandCanvas() {
        commandCanvas.enabled = false;
        targetingCanvas.enabled = false;
        messageCanvas.enabled = true;
        spellCanvas.enabled = false;
        itemCanvas.enabled = false;
    }

    public void UpdateAllStats() {
        foreach(KeyValuePair<StatsBase, CharacterPanel> pair in statsPanels) {
            pair.Value.UpdateStats();
        }
    }

    IEnumerator StartBattle(List<EnemyStats> enemies) {
        DisableCommandCanvas();

        // Reset values
        battleEnded = false;
        playerTeam.Clear();
        allAllies.Clear();
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
            allAllies.Add(temp);

            if (!player.isDead) {
                playerTeam.Add(temp);
            }
        }
        foreach (StatsBase player in playerTeam) {
            turnOrder.Add(player);
        }

        if (enemies != null) {
            AddSetEnemies(enemies);
        }
        else {
            AddRandomEnemies();
        }

        // Sort by agility
        turnOrder = turnOrder.OrderByDescending(stat => stat.currentAGI).ToList();

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

        while (!GameManager.instance.transition.textureHidden) {
            yield return null;
        }

        yield return new WaitForSeconds(timeToWait * Mathf.CeilToInt(enemyTeam.Count / 2));
        NextTurn();
    }

    void AddRandomEnemies() {
        // Initialise enemies
        int enemyCount = Mathf.Min(Random.Range(1, dungeon.getFloorNumber() + 3), (int)numberOfEnemies.Evaluate(Random.Range(0f, 1f)));
        Dictionary<string, int> enemyDict = new Dictionary<string, int>();
        for (int i = 0; i < enemyCount; i++) {
            EnemyStats temp = Instantiate(possibleEnemies[Random.Range(0, Mathf.Clamp(Mathf.CeilToInt((dungeon.getFloorNumber() + 1) / 5f) * 5, 0, possibleEnemies.Count))]);
            temp.level = dungeon.getFloorNumber() + 1;
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
    }

    void AddSetEnemies(List<EnemyStats> enemies) {
        foreach (EnemyStats enemy in enemies) { 
            EnemyStats temp = Instantiate(enemy);
            temp.InitialiseCharacter();
            enemyTeam.Add(temp);
            allEnemies.Add(temp);
        }

        foreach (StatsBase enemy in enemyTeam) {
            turnOrder.Add(enemy);
        }
    }

    IEnumerator EndBattle(bool keepGold) {
        if (!gameOver) { 
            if (keepGold) {
                PartyManager.instance.gold += goldEarned;
                messageText.text += string.Format("The party has earned {0} experience points and {1} gold.", expEarned, goldEarned);
            }
            else {
                messageText.text += string.Format("The party has earned {0} experience points.", expEarned);
            }
            yield return new WaitForSeconds(timeToWait);

            for (int i = 0; i < PartyManager.instance.partyMembers.Count; i++) {
                PlayerStats player = (PlayerStats)statsPanels.Keys.ElementAt<StatsBase>(i);

                if (!player.isDead) {
                    player.totalXP += expEarned;
                    while (player.totalXP >= player.targetXP) {
                        yield return new WaitForSeconds(timeToWait);
                        player.level++;
                        player.targetXP += (player.level * 50);
                        messageText.text = string.Format("{0}'s level has increased to {1}!", player.characterName, player.level);
                        AudioPlayer.Instance.LevelUp();
                        yield return new WaitForSeconds(timeToWait * 2);
                        messageText.text = string.Format("{0}'s stats have increased.\n HP + {1}, MP + {2}, ATK + {3},\nDEF + {4}, AGI + {5}, INT + {6}.",
                            player.characterName, player.hpPerLevel, player.mpPerLevel, player.atkPerLevel, player.defPerLevel, player.agiPerLevel, player.intPerLevel);
                        yield return new WaitForSeconds(timeToWait);
                        player.InitialiseCharacter();
                        player.currentHP = player.maxHP;
                        player.currentMP = player.maxMP;
                        UpdateAllStats();
                    }
                }


                PartyManager.instance.partyMembers[i].currentHP = player.currentHP;
                PartyManager.instance.partyMembers[i].currentMP = player.currentMP;
                PartyManager.instance.partyMembers[i].level = player.level;
                PartyManager.instance.partyMembers[i].totalXP = player.totalXP;
                PartyManager.instance.partyMembers[i].targetXP = player.targetXP;
                PartyManager.instance.partyMembers[i].isDead = player.isDead;
                PartyManager.instance.partyMembers[i].spellList = player.spellList;
                PartyManager.instance.partyMembers[i].armour = player.armour;
                PartyManager.instance.partyMembers[i].weapon = player.weapon;
                InventoryManager.Instance.equipment[i].armorSlot.item = PartyManager.instance.partyMembers[i].armour;
                InventoryManager.Instance.equipment[i].weaponSlot.item = PartyManager.instance.partyMembers[i].weapon;
            }

            expEarned = 0;
            goldEarned = 0;

            yield return new WaitForSeconds(timeToWait * 2);

            GameManager.instance.transition.BeginShow();
            GameManager.instance.player.canMove = false;
            while (!GameManager.instance.transition.textureShown) {
                yield return null;
            }
            GameManager.instance.transition.BeginHide();
            GameManager.instance.player.canMove = true;

            if (escapePoint) {
                GameManager.instance.player.transform.position = escapePoint.position;
                GameManager.instance.player.lerping = false;
            }

            GameManager.instance.EndCombat();

            if (isBossEncounter && keepGold) {
                GameManager.instance.CompleteGame();
            }

        }

        else {
            GameManager.instance.transition.BeginShow();
            GameManager.instance.player.canMove = false;
            while (!GameManager.instance.transition.textureShown) {
                yield return null;
            }
            GameManager.instance.transition.BeginHide();
            GameManager.instance.player.canMove = true;

            GameManager.instance.GameOver();
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsBase : ScriptableObject
{
    public string characterName;
    public Sprite sprite;
    public int level, maxHP, maxMP, baseATK, baseDEF, baseAGI, baseINT, expValue, goldValue, totalXP, targetXP;
    public int currentHP, currentMP, currentATK, currentDEF, currentAGI, currentINT;
    public int hpPerLevel, mpPerLevel, atkPerLevel, defPerLevel, agiPerLevel, intPerLevel;
    [Range(0, 1)]
    public float critChance;
    public bool isDead, isEnemy;
    public List<Spell> spellList = new List<Spell>();
    protected bool isDefending;
    public int initialisedLevel;

    public void InitialiseCharacter() {
        if (initialisedLevel < level) {
            initialisedLevel = level;
            maxHP = maxHP + (hpPerLevel * (level - 1));
            maxMP = maxMP + (mpPerLevel * (level - 1));
            currentATK = baseATK + (atkPerLevel * (level - 1)) + InventoryManager.Instance.equipment.weaponSlot.item.primaryStatValue;
            currentDEF = baseDEF + (defPerLevel * (level - 1)) + InventoryManager.Instance.equipment.armorSlot.item.primaryStatValue;
            currentAGI = baseAGI + (agiPerLevel * (level - 1));
            currentINT = baseINT + (intPerLevel * (level - 1));

            SetCurrentHPMP();

            targetXP = 0;
            int tempTotal = 0;
            for (int i = 0; i <= level; i++) {
                tempTotal = targetXP;
                targetXP += (i * 50);
            }
            if (level > 1 && totalXP == 0) {
                totalXP = tempTotal;
            }
        }
    }

    public void SetCurrentHPMP() {
        if (!isDead && currentHP == 0) {
            currentHP = maxHP;
            currentMP = maxMP;
        }
    }

    public int TakeDamage(int damage, bool ignoreDefence) {
        if (!isDead) {
            // Adjust damage based on defence stat
            damage += (int)Random.Range(-(damage * 0.11f), damage * 0.21f);
            int defenceAdjusted = currentDEF;
            if (isDefending) {
                defenceAdjusted *= 2;
            }
            if (currentDEF <= 0 || ignoreDefence) {
                defenceAdjusted = 1;
            }
            int damageToTake = damage;
            if (defenceAdjusted > damageToTake) {
                damageToTake -= (defenceAdjusted - damageToTake);
            }
            if (damageToTake < 0) {
                damageToTake = 0;
            }

            if (damageToTake == 0 && Random.Range(0, 2) == 1) {
                damageToTake = 1;
            }
            else if (damageToTake > 999) {
                damageToTake = 999;
            }

            ChangeHealth(-damageToTake);

            return damageToTake;
        }

        return 0;
    }

    public int Heal(int amount) {
        Debug.Log("healing!");
        ChangeHealth(amount);
        return amount;
    }

    private void ChangeHealth(int amount) {
        // Deal damage, maybe die
        currentHP += amount;
        if (currentHP <= 0) {
            Kill();
        }

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    private void SetHealth(int amount) {
        // Deal damage, maybe die
        currentHP = amount;
        if (currentHP <= 0) {
            Kill();
        }

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public virtual void Kill() {
        isDead = true;

        CombatManager.instance.expEarned += expValue * (1 + Mathf.CeilToInt(level * 0.75f));
        CombatManager.instance.goldEarned += goldValue * (1 + Mathf.CeilToInt(level * 0.75f));
    }

    public virtual void GetCommand() {
        isDefending = false;
    }

    public void Attack(StatsBase target) {
        Attack(target, currentATK);
    }

    public void Attack(StatsBase target, int damage) {
        bool isCrit = false;
        if (Random.Range(0f, 1f) <= critChance) {
            isCrit = true;
        }

        CombatManager.instance.StartAttack(this, target, damage, isCrit);
    }

    public void Defend() {
        isDefending = true;
        CombatManager.instance.Defend(this);
    }

    public void CastSpell(StatsBase target, Spell spell) {
        CombatManager.instance.SpellAttack(this, target, spell);
    }

    public void UseItem(StatsBase target, Item item) {
        CombatManager.instance.UseItem(this, target, item);
    }
}

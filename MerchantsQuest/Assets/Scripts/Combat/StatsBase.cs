﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsBase : ScriptableObject
{
    public string characterName;
    public Sprite sprite;
    public int level, maxHP, maxMP, baseATK, baseDEF, baseAGI, expValue;
    [HideInInspector]
    public int currentHP, currentMP, currentATK, currentDEF, currentAGI;
    public int hpPerLevel, mpPerLevel, atkPerLevel, defPerLevel, agiPerLevel;
    [Range(0, 1)]
    public float critChance;
    public bool isDead, isEnemy;
    protected bool isDefending;

    public void InitialiseCharacter() {
        currentATK = baseATK + (atkPerLevel * (level - 1));
        currentDEF = baseDEF + (defPerLevel * (level - 1));
        currentAGI = baseAGI + (agiPerLevel * (level - 1));
        if (!isDead && currentHP == 0) {
            maxHP = maxHP + (hpPerLevel * (level - 1));
            maxMP = maxMP + (mpPerLevel * (level - 1));
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
            int damageToTake = damage / defenceAdjusted;
            if (damageToTake == 0 && Random.Range(0, 2) == 1) {
                damageToTake = 1;
            }

            ChangeHealth(damageToTake);

            return damageToTake;
        }

        return 0;
    }

    private void ChangeHealth(int amount) {
        // Deal damage, maybe die
        currentHP -= amount;
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

        CombatManager.instance.turnOrder.Remove(this);
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

        CombatManager.instance.Attack(this, target, damage, isCrit);
    }

    public void Defend() {
        isDefending = true;
        CombatManager.instance.Defend(this);
    }
}

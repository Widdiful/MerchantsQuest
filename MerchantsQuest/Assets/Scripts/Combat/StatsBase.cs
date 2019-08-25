using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsBase
{
    public string characterName;
    public int level, hitPoints, magicPoints, attack, defence, agility;
    [Range(0, 1)]
    public float critChance;
    public bool isDead, isEnemy;
    private bool isDefending;

    public int TakeDamage(int damage, bool ignoreDefence) {
        if (!isDead) {
            // Adjust damage based on defence stat
            int defenceAdjusted = defence;
            if (isDefending) {
                defenceAdjusted *= 2;
            }
            if (defence <= 0 || ignoreDefence) {
                defenceAdjusted = 1;
            }
            int damageToTake = damage / defenceAdjusted;
            if (damageToTake == 0 && Random.Range(0, 2) == 1) {
                damageToTake = 1;
            }

            // Deal damage, maybe die
            hitPoints -= damageToTake;
            if (hitPoints <= 0) {
                hitPoints = 0;
                Kill();
            }

            return damageToTake;
        }

        return 0;
    }

    public virtual void Kill() {
        isDead = true;

        CombatManager.instance.turnOrder.Remove(this);
    }

    public virtual void GetCommand() {
        isDefending = false;
    }

    public void Attack(StatsBase target) {
        Attack(target, attack);
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

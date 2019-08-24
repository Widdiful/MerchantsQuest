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

    public int TakeDamage(int damage, bool ignoreDefence) {
        if (!isDead) {
            // Adjust damage based on defence stat
            int defenceAdjusted = defence;
            if (defence <= 0 || ignoreDefence) {
                defenceAdjusted = 1;
            }
            int damageToTake = damage / defenceAdjusted;

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

    public void Kill() {
        isDead = true;

        Debug.Log(characterName + " died!");

        CombatManager.instance.turnOrder.Remove(this);
        if (isEnemy) {
            CombatManager.instance.enemyTeam.Remove(this);
        }
        else {
            CombatManager.instance.playerTeam.Remove(this);
        }
    }

    public virtual void GetCommand() {
        if (isEnemy)
            Attack(CombatManager.instance.playerTeam[0], attack);
        else
            Attack(CombatManager.instance.enemyTeam[0], attack);
    }

    public void Attack(StatsBase target, int damage) {
        bool isCrit = false;
        if (Random.Range(0f, 1f) <= critChance) {
            isCrit = true;
        }

        CombatManager.instance.Attack(this, target, damage, isCrit);
    }
}

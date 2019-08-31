﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class EnemyStats : StatsBase
{
    private StatsBase target;
    [Range(0,1)]
    public float chanceToUseSpell;
    [Range(0, 1)]
    public float blockChance;
    public Vector2 spriteSize;

    public override void GetCommand() {
        base.GetCommand();

        target = CombatManager.instance.playerTeam[Random.Range(0, CombatManager.instance.playerTeam.Count)];

        if (spellList.Count > 0 && Random.Range(0f, 1f) <= chanceToUseSpell) {
            Spell spellToCast = spellList[Random.Range(0, spellList.Count)];
            if (spellToCast.spellType == SpellType.Heal) {
                target = CombatManager.instance.enemyTeam[Random.Range(0, CombatManager.instance.enemyTeam.Count)];
            }
            CastSpell(target, spellToCast);
        }

        else if (Random.Range(0f, 1f) <= blockChance) {
            Defend();
        }

        else {
            Attack(target);
        }
    }

    public override void Kill() {
        base.Kill();

        CombatManager.instance.enemyTeam.Remove(this);
    }
}

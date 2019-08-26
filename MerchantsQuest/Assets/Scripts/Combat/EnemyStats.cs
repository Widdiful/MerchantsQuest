using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class EnemyStats : StatsBase
{
    private PlayerStats target;
    public float chanceToUseSpell;
    public float blockChance;

    public override void GetCommand() {
        base.GetCommand();

        target = CombatManager.instance.playerTeam[Random.Range(0, CombatManager.instance.playerTeam.Count)];

        if (spellList.Count > 0 && Random.Range(0f, 1f) <= chanceToUseSpell) {
            Spell spellToCast = spellList[Random.Range(0, spellList.Count)];
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

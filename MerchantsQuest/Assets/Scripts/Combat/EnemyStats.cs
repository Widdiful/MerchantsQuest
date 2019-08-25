using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class EnemyStats : StatsBase
{
    private PlayerStats target;

    public override void GetCommand() {
        base.GetCommand();

        target = CombatManager.instance.playerTeam[Random.Range(0, CombatManager.instance.playerTeam.Count)];

        Attack(target);
    }

    public override void Kill() {
        base.Kill();

        CombatManager.instance.enemyTeam.Remove(this);
    }
}

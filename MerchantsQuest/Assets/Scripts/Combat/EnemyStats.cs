using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyStats : StatsBase
{
    public override void GetCommand() {
        base.GetCommand();

        Attack(CombatManager.instance.playerTeam[0]);
    }

    public override void Kill() {
        base.Kill();

        CombatManager.instance.enemyTeam.Remove(this);
    }
}

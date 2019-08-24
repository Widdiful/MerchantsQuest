﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyStats : StatsBase
{
    public override void GetCommand() {
        base.GetCommand();


    }

    public override void Kill() {
        base.Kill();

        CombatManager.instance.enemyTeam.Remove(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : StatsBase
{
    public override void Kill() {
        base.Kill();

        CombatManager.instance.playerTeam.Remove(this);
    }

    public override void GetCommand() {
        base.GetCommand();

        CombatManager.instance.EnableCommandCanvas();
    }
}

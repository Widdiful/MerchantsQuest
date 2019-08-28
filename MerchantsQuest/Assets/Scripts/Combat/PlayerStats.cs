using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlayerStats : StatsBase
{
    public Item weapon, armour;

    public override void Kill() {
        base.Kill();

        CombatManager.instance.playerTeam.Remove(this);
    }

    public override void GetCommand() {
        base.GetCommand();

        CombatManager.instance.EnableCommandCanvas();
    }

    public override void InitialiseCharacter() {
        base.InitialiseCharacter();
        currentATK = baseATK + (atkPerLevel * (level - 1)) + weapon.primaryStatValue;
        currentDEF = baseDEF + (defPerLevel * (level - 1)) + armour.primaryStatValue;
    }
}

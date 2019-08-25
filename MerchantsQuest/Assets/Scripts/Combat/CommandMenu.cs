using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    public enum CommandType { Attack, Spell, Item };
    public StatsBase target;
    public CommandType commandType;

    public void ExecuteCommand() {
        switch (commandType) {
            case CommandType.Attack:
                CombatManager.instance.currentActor.Attack(target);
                break;
        }
    }

    public void AttackTest() {
        commandType = CommandType.Attack;
        target = CombatManager.instance.enemyTeam[0];
        ExecuteCommand();
    }
}

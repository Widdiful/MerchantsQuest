using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    public enum CommandType { Attack, Defend, Spell, Item };
    public StatsBase target;
    public CommandType commandType;
    public Canvas commandCanvas, targetingCanvas;
    public TargetingMenu targetingMenu;

    public void ExecuteCommand() {
        switch (commandType) {
            case CommandType.Attack:
                CombatManager.instance.currentActor.Attack(target);
                break;
            case CommandType.Defend:
                CombatManager.instance.currentActor.Defend();
                break;
        }
        CombatManager.instance.ToggleIndicator();
    }

    private void SwitchToTargeting() {
        commandCanvas.enabled = false;
        targetingCanvas.enabled = true;
        targetingMenu.InitialiseTargetList();
    }

    public void SetTarget(int index) {
        target = CombatManager.instance.enemyTeam[index];
        ExecuteCommand();
    }

    public void AttackButton() {
        commandType = CommandType.Attack;
        SwitchToTargeting();
    }

    public void DefendButton() {
        commandType = CommandType.Defend;
        ExecuteCommand();
    }
}

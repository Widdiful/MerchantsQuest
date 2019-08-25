using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    public enum CommandType { Attack, Spell, Item };
    public StatsBase target;
    public CommandType commandType;
    public Canvas commandCanvas, targetingCanvas;
    public TargetingMenu targetingMenu;

    public void ExecuteCommand() {
        switch (commandType) {
            case CommandType.Attack:
                CombatManager.instance.currentActor.Attack(target);
                break;
        }
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
}

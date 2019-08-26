using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    public enum CommandType { Attack, Defend, Spell, Item };
    public StatsBase target;
    public CommandType commandType;
    public Spell currentSpell;
    public Canvas commandCanvas, targetingCanvas, spellCanvas;
    public TargetingMenu targetingMenu;
    public SpellMenu spellMenu;

    public void ExecuteCommand() {
        switch (commandType) {
            case CommandType.Attack:
                CombatManager.instance.currentActor.Attack(target);
                break;
            case CommandType.Spell:
                CombatManager.instance.currentActor.CastSpell(target, currentSpell);
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
        spellCanvas.enabled = false;
        targetingMenu.InitialiseTargetList();
    }

    private void SwitchToSpell() {
        commandCanvas.enabled = false;
        targetingCanvas.enabled = false;
        spellCanvas.enabled = true;
        spellMenu.InitialiseTargetList();
    }

    public void SetSpell(Spell spell) {
        currentSpell = spell;
        SwitchToTargeting();
    }

    public void SetTarget(int index) {
        target = CombatManager.instance.enemyTeam[index];
        ExecuteCommand();
    }

    public void AttackButton() {
        commandType = CommandType.Attack;
        SwitchToTargeting();
    }

    public void SpellButton() {
        commandType = CommandType.Spell;
        SwitchToSpell();
    }

    public void DefendButton() {
        commandType = CommandType.Defend;
        ExecuteCommand();
    }
}

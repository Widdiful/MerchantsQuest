using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMenu : MonoBehaviour
{
    public enum CommandType { Attack, Defend, Spell, Item, Flee};
    public StatsBase target;
    public CommandType commandType;
    public Spell currentSpell;
    public Item currentItem;
    public Canvas commandCanvas, targetingCanvas, spellCanvas, itemCanvas;
    public TargetingMenu targetingMenu;
    public SpellMenu spellMenu;
    public ItemMenu itemMenu;

    public void ExecuteCommand() {
        switch (commandType) {
            case CommandType.Attack:
                CombatManager.instance.currentActor.Attack(target);
                break;
            case CommandType.Spell:
                CombatManager.instance.currentActor.CastSpell(target, currentSpell);
                break;
            case CommandType.Item:
                CombatManager.instance.currentActor.UseItem(target, currentItem);
                break;
            case CommandType.Defend:
                CombatManager.instance.currentActor.Defend();
                break;
            case CommandType.Flee:
                CombatManager.instance.Flee();
                break;
        }
        CombatManager.instance.ToggleIndicator();
    }

    private void SwitchToTargeting(bool targetAllies) {
        commandCanvas.enabled = false;
        targetingCanvas.enabled = true;
        spellCanvas.enabled = false;
        itemCanvas.enabled = false;
        targetingMenu.InitialiseTargetList(targetAllies);
    }

    private void SwitchToSpell() {
        commandCanvas.enabled = false;
        targetingCanvas.enabled = false;
        spellCanvas.enabled = true;
        itemCanvas.enabled = false;
        spellMenu.InitialiseTargetList();
    }

    public void SetSpell(Spell spell) {
        currentSpell = spell;
        SwitchToTargeting(spell.spellType == SpellType.Heal);
    }

    private void SwitchToItem() {
        commandCanvas.enabled = false;
        targetingCanvas.enabled = false;
        spellCanvas.enabled = false;
        itemCanvas.enabled = true;
        itemMenu.InitialiseTargetList();
    }

    public void SetItem(Item item) {
        currentItem = item;
        SwitchToTargeting(item.spellType == SpellType.Heal);
    }

    public void SetTarget(int index, bool targetAllies) {
        if (!targetAllies) {
            target = CombatManager.instance.enemyTeam[index];
        }
        else {
            target = CombatManager.instance.allAllies[index];
        }
        ExecuteCommand();
    }

    public void AttackButton() {
        commandType = CommandType.Attack;
        SwitchToTargeting(false);
    }

    public void SpellButton() {
        commandType = CommandType.Spell;
        SwitchToSpell();
    }

    public void ItemButton() {
        commandType = CommandType.Item;
        SwitchToItem();
    }

    public void DefendButton() {
        commandType = CommandType.Defend;
        ExecuteCommand();
    }

    public void FleeButton() {
        commandType = CommandType.Flee;
        ExecuteCommand();
    }
}

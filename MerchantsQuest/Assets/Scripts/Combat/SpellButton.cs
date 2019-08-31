using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    public int index;
    public string characterName;
    public TextMeshProUGUI text;
    public Spell spell;
    public Button button;
    private CommandMenu commandMenu;
    private TargetingMenu targetingMenu;
    private Canvas targetingCanvas;
    private bool overworldSpell;
    public Canvas thisCanvas;
    private StatsBase spellCaster;

    public void Initialise(int newIndex, CommandMenu menu, Spell newSpell, bool canAfford) {
        index = newIndex;
        characterName = newSpell.name;
        text.text = characterName;
        commandMenu = menu;
        spell = newSpell;
        button.interactable = canAfford;
    }

    public void SetOverworldSpell(TargetingMenu menu, Canvas canvas, StatsBase caster) {
        targetingMenu = menu;
        targetingCanvas = canvas;
        overworldSpell = true;
        spellCaster = caster;
    }

    public void Initialise(string name) {
        text.text = name;
        button.interactable = false;
    }

    public void Click() {
        if (!overworldSpell) {
            commandMenu.SetSpell(spell);
        }
        else {
            targetingMenu.SetSpell(spell, spellCaster);
            targetingMenu.InitialiseTargetList(true);
            targetingCanvas.enabled = true;
            thisCanvas.enabled = false;
        }
    }
}

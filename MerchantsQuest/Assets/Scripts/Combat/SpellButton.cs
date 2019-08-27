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

    public void Initialise(int newIndex, CommandMenu menu, Spell newSpell, bool canAfford) {
        index = newIndex;
        characterName = newSpell.name;
        text.text = characterName;
        commandMenu = menu;
        spell = newSpell;
        button.interactable = canAfford;
    }

    public void Click() {
        commandMenu.SetSpell(spell);
    }
}

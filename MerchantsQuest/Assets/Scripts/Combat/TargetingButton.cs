using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetingButton : MonoBehaviour
{
    public int index;
    public string characterName;
    public TextMeshProUGUI text;
    private CommandMenu commandMenu;
    private bool targetAllies;
    private bool overworldTarget;
    private Spell currentSpell;
    private StatsBase spellCaster;

    public void Initialise(int newIndex, string newName, CommandMenu menu, bool newTargetAllies) {
        index = newIndex;
        characterName = newName;
        text.text = characterName;
        commandMenu = menu;
        targetAllies = newTargetAllies;
    }

    public void SetSpell(Spell spell, StatsBase caster) {
        overworldTarget = true;
        currentSpell = spell;
        spellCaster = caster;
    }

    public void Click() {
        if (!overworldTarget) {
            commandMenu.SetTarget(index, targetAllies);
        }
        else {
            if (spellCaster.currentMP >= currentSpell.manaCost) {
                if (PartyManager.instance.partyMembers[index].currentHP < PartyManager.instance.partyMembers[index].maxHP) {
                    spellCaster.currentMP -= currentSpell.manaCost;
                    PartyManager.instance.partyMembers[index].Heal(currentSpell.primaryStatValue + PartyManager.instance.partyMembers[index].currentINT);
                    SpellInventory.Instance.AppraiseSpell(currentSpell.id);
                    MessageBox.instance.NewMessage(string.Format("{0} healed {1} HP.", PartyManager.instance.partyMembers[index].characterName, currentSpell.primaryStatValue));
                }
                else {
                    MessageBox.instance.NewMessage(string.Format("HP already full."));
                }
            }
            else {
                MessageBox.instance.NewMessage(string.Format("Not enough MP."));
            }
        }
    }
}

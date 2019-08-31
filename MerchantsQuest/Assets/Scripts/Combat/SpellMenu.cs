using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform listContainer;
    public CommandMenu commandMenu;
    public bool overworldSpell;
    public TargetingMenu targetingMenu;
    public Canvas targetingCanvas, thisCanvas;

    public void InitialiseTargetList() {
        foreach(Transform transform in listContainer) {
            Destroy(transform.gameObject);
        }

        if (!overworldSpell) {
            for (int i = 0; i < CombatManager.instance.currentActor.spellList.Count; i++) {
                SpellButton button = Instantiate(buttonPrefab, listContainer).GetComponent<SpellButton>();
                bool canAfford = CombatManager.instance.currentActor.currentMP >= CombatManager.instance.currentActor.spellList[i].manaCost;
                button.Initialise(i, commandMenu, CombatManager.instance.currentActor.spellList[i], canAfford);
            }
        }

        else {
            for (int i = 0; i < PartyManager.instance.partyMembers.Count; i++) {
                SpellButton namePlate = Instantiate(buttonPrefab, listContainer).GetComponent<SpellButton>();
                namePlate.Initialise(PartyManager.instance.partyMembers[i].characterName);

                for (int j = 0; j < PartyManager.instance.partyMembers[i].spellList.Count; j++) {                    
                    if (PartyManager.instance.partyMembers[i].spellList[j].spellType == SpellType.Heal && !PartyManager.instance.partyMembers[i].spellList[j].attackAll) {
                        SpellButton button = Instantiate(buttonPrefab, listContainer).GetComponent<SpellButton>();
                        button.thisCanvas = thisCanvas;
                        bool canAfford = PartyManager.instance.partyMembers[i].currentMP >= PartyManager.instance.partyMembers[i].spellList[j].manaCost;
                        button.Initialise(j, commandMenu, PartyManager.instance.partyMembers[i].spellList[j], canAfford);
                        button.SetOverworldSpell(targetingMenu, targetingCanvas, PartyManager.instance.partyMembers[i]);
                    }
                }
            }
        }
    }
}

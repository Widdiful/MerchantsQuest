using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform listContainer;
    public CommandMenu commandMenu;

    public void InitialiseTargetList() {
        foreach(Transform transform in listContainer) {
            Destroy(transform.gameObject);
        }

        for (int i = 0; i < CombatManager.instance.currentActor.spellList.Count; i++) { 
            SpellButton button = Instantiate(buttonPrefab, listContainer).GetComponent<SpellButton>();
            bool canAfford = CombatManager.instance.currentActor.currentMP >= CombatManager.instance.currentActor.spellList[i].manaCost;
            button.Initialise(i, commandMenu, CombatManager.instance.currentActor.spellList[i], canAfford);
        }
    }
}

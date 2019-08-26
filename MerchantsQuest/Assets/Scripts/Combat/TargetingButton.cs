using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetingButton : MonoBehaviour
{
    public int index;
    public string characterName;
    public Text text;
    private CommandMenu commandMenu;
    private bool targetAllies;

    public void Initialise(int newIndex, string newName, CommandMenu menu, bool newTargetAllies) {
        index = newIndex;
        characterName = newName;
        text.text = characterName;
        commandMenu = menu;
        targetAllies = newTargetAllies;
    }

    public void Click() {
        commandMenu.SetTarget(index, targetAllies);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public int index;
    public string characterName;
    public TextMeshProUGUI text;
    public Item item;
    public Button button;
    private CommandMenu commandMenu;

    public void Initialise(int newIndex, CommandMenu menu, Item newItem) {
        index = newIndex;
        characterName = newItem.name;
        text.text = characterName;
        commandMenu = menu;
        item = newItem;
    }

    public void Click() {
        commandMenu.SetItem(item);
    }
}

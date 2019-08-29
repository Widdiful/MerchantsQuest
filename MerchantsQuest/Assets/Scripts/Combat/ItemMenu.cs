using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform listContainer;
    public CommandMenu commandMenu;

    public void InitialiseTargetList() {
        foreach(Transform transform in listContainer) {
            Destroy(transform.gameObject);
        }

        for (int i = 0; i < InventoryManager.Instance.inventorySlots.Length; i++) { 
            if (!InventoryManager.Instance.inventorySlots[i].empty) {
                ItemButton button = Instantiate(buttonPrefab, listContainer).GetComponent<ItemButton>();
                button.Initialise(i, commandMenu, InventoryManager.Instance.inventorySlots[i].item);
            }
            
        }
    }
}

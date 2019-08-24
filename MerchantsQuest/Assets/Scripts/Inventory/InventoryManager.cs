using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private bool inventoryEnabled;
    public GameObject inventory;
    private int availableSlots;

    private InventorySlot[] inventorySlots;

    private void Awake()
    {
        inventorySlots = new InventorySlot[Constants.maxInventorySize];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled)
            inventory.SetActive(true);
        else
            inventory.SetActive(false);
    }

    public void AddItem(int id)
    {

    }

}

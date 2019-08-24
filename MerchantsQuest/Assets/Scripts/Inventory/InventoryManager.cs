using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private bool inventoryEnabled;
    public GameObject inventory;
    private int availableSlots;
    private int totalSlots;

    private void Awake()
    {
        totalSlots = Constants.maxInventorySize;
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

}

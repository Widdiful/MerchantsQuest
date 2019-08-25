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
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

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
        for(int i = 0; i < Constants.maxInventorySize; i++)
        {
            if(inventorySlots[i].empty)
            {
                inventorySlots[i].SetItem(id);
                return;
            }
        }

        Debug.Log("All slots full");
    }


    [ContextMenu("Item Add Test")]
    public void AddRandomItem()
    {
        for (int i = 0; i < Constants.maxInventorySize; i++)
        {
            if (inventorySlots[i].empty)
            {
                inventorySlots[i].SetItem(Random.Range(0, 100));
                return;
            }
        }
        Debug.Log("All slots full");
    }
}

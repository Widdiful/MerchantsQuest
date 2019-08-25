using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    private bool inventoryEnabled;
    public GameObject inventory;
    private int availableSlots;

    public InventorySlot[] inventorySlots;

    public StatShower statShower;

    public EquipmentManager equipment;

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
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].empty)
            {
                inventorySlots[i].SetItem(id);
                return;
            }
        }

        Debug.Log("All slots full");
    }

    public void RemoveItem(int id)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].itemInSlot.id == id)
            {
                inventorySlots[i].RemoveItem();
                return;
            }
        }
    }


    [ContextMenu("Item Add Test")]
    public void AddRandomItem()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].empty)
            {
                inventorySlots[i].SetItem(Random.Range(0, 100));
                return;
            }
        }
        Debug.Log("All slots full");
    }

    public void DisplayStats(Item item, Vector2 pos)
    {
        statShower.gameObject.SetActive(true);
        statShower.SetStats(item, pos);
    }

    public void HideStats()
    {
        statShower.gameObject.SetActive(false);
    }
}

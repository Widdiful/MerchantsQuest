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

    public GameObject equipPanel;
    InventorySlot itemToEquip;
    public EquipmentManager[] equipment;


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
    }

    private void Update() {
        if (inventoryEnabled && Input.GetKeyDown("escape")) {
            ToggleMenu();
        }
    }

    public void ToggleMenu() {
        inventoryEnabled = !inventoryEnabled;

        if (!inventoryEnabled) {
            inventory.SetActive(false);
            HideStats();
            GameManager.instance.player.canMove = true;
        }

        if (inventoryEnabled) {
            inventory.SetActive(true);
            GameManager.instance.player.canMove = false;
            
        }
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].empty)
            {
                inventorySlots[i].SetSlot(item);
                return;
            }
        }

        Debug.Log("All slots full");
    }

    public void RemoveItem(int id)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].item.id == id)
            {
                inventorySlots[i].RemoveItem();
                return;
            }
        }
    }



    [ContextMenu("Fill Inventory")]
    public void FillInventory()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SetSlot(ItemManager.Instance.GetItemFromID(i));
        }
    }

    public List<Item> GetAllOfType(ItemType type)
    {
        List<Item> items = new List<Item>();

        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if(!inventorySlots[i].empty)
            {
                if(inventorySlots[i].item.type == type)
                {
                    items.Add(inventorySlots[i].item);
                }
            }
        }
        return items;
    }

    public void DisplayStats(Item item, RectTransform pos, int cost = 0)
    {
        statShower.gameObject.SetActive(true);
        statShower.SetStats(item, pos, cost);
    }

    public void DisplaySpellStats(Spell spell, RectTransform pos)
    {
        statShower.gameObject.SetActive(true);
        statShower.SetSpellStats(spell, pos);
    }

    public void HideStats()
    {
        statShower.gameObject.SetActive(false);
    }

    public void AppraiseItem(int id)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].item.id == id)
            {
                inventorySlots[i].item.appraised = true;
                ItemManager.Instance.UpdateItem(id, inventorySlots[i].item);
            }
        }
    }

    public void ShowEquipPanel(InventorySlot item)
    {
        itemToEquip = item;
        equipPanel.SetActive(true);
    }

    public void CancelEquipPanel ()
    {
        equipPanel.SetActive(false);
    }

    public void EquipItem(EquipmentManager character)
    {
        if(!PartyManager.instance.partyMembers[character.player].isDead)
            character.AddToSlot(itemToEquip.item, itemToEquip.item.type);
        equipPanel.SetActive(false);
        itemToEquip.RemoveItem();
        itemToEquip = null;
    }
}

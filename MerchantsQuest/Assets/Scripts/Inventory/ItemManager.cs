using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;



//Stores all items with ids
public class ItemManager : MonoBehaviour
{
    [SerializeField]
    public Dictionary<int, Item> items;

    public static ItemManager Instance;

    //Data to be used for generating items
    public ItemRefs armorRefs;
    public ItemRefs consumeableRefs;
    public ItemRefs spellRefs;
    public ItemRefs weaponRefs;

    public System.Random rand;

    public int itemAmount;
    public int currentDungeonBestFloor;
    private void Awake()
    {
        rand = new System.Random(Random.Range(0, 10000000));
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


        SetUpRefs();
        items = new Dictionary<int, Item>();
        currentDungeonBestFloor = 1;
    }

    private void SetUpRefs()
    {
        if (armorRefs != null)
        {
            armorRefs.descriptions = Helpers.ReadFile(armorRefs.fakeDescriptionsAsset);
            armorRefs.names = Helpers.ReadFile(armorRefs.nameAsset);
        }

        if (consumeableRefs != null)
        {
            consumeableRefs.descriptions = Helpers.ReadFile(consumeableRefs.fakeDescriptionsAsset);
            consumeableRefs.names = Helpers.ReadFile(consumeableRefs.nameAsset);
        }

        if(spellRefs != null)
        {
            spellRefs.descriptions = Helpers.ReadFile(spellRefs.fakeDescriptionsAsset);
            spellRefs.names = Helpers.ReadFile(spellRefs.nameAsset);
        }

        if (weaponRefs != null)
        {
            weaponRefs.descriptions = Helpers.ReadFile(weaponRefs.fakeDescriptionsAsset);
            weaponRefs.names = Helpers.ReadFile(weaponRefs.nameAsset);
        }
    }

    [ContextMenu("Test Generation")]
    public void GenerateItems()
    {
        //This is a big to do
        for (int i = 0; i < itemAmount; i++)
        {
            Item item = new Item();
            item.appraised = false;
            item.type = (ItemType)rand.Next((int)ItemType.Size);
            item = GetData(item);
            item.id = i;

            items.Add(i, item);
            Debug.Log(i);
        }
    }

    public Item GenerateSpecificItem(ItemType type)
    {
        Item item = new Item();
        item.appraised = false;
        item.type = type;
        item = GetData(item);
        item.id = items.Count + 1;

        items.Add(item.id, item);

        return item;

    }

    [ContextMenu("Show Items")]
    public void PrintItems()
    {
        for(int i = 0; i < itemAmount; i++)
        {
            Debug.Log("ID = " + items[i].id + ", fake description = " + items[i].fakeDescription +
                ", Type = " + items[i].type.ToString() + ", Real stat = " + items[i].primaryStat);
        }
    }

    private Item GetData(Item currItem)
    {
        switch (currItem.type)
        {
            case ItemType.Spell:
                currItem.name = spellRefs.names[rand.Next(spellRefs.names.Length)];
                currItem.fakeDescription = spellRefs.descriptions[rand.Next(spellRefs.descriptions.Length)];
                currItem.icon = spellRefs.sprites[rand.Next(spellRefs.sprites.Length)];
                currItem.primaryStatValue = Random.Range(1, 10) * currentDungeonBestFloor;
                currItem.spellType = (SpellType)Random.Range(1, (int)SpellType.Size);
                currItem.manaCost = Random.Range(1, 5) * currentDungeonBestFloor;
                
                break;
            case ItemType.Weapon:
                currItem.name = weaponRefs.names[rand.Next(weaponRefs.names.Length)];
                currItem.fakeDescription = weaponRefs.descriptions[rand.Next(weaponRefs.descriptions.Length)];
                currItem.icon = weaponRefs.sprites[rand.Next(weaponRefs.sprites.Length)];
                currItem.primaryStat = StatType.Attack;
                currItem.primaryStatValue = Random.Range(1, 10) * currentDungeonBestFloor;
                currItem.secondaryStat = (StatType)Random.Range(0, (int)StatType.Size);
                currItem.secondaryStatValue = Random.Range(0, 5) * currentDungeonBestFloor;
                break;
            case ItemType.Armor:
                currItem.name = armorRefs.names[rand.Next(armorRefs.names.Length)];
                currItem.fakeDescription = armorRefs.descriptions[rand.Next(armorRefs.descriptions.Length)];
                currItem.icon = armorRefs.sprites[rand.Next(armorRefs.sprites.Length)];
                currItem.primaryStat = StatType.Defence;
                currItem.primaryStatValue = Random.Range(1, 10) * currentDungeonBestFloor;
                currItem.secondaryStat = (StatType)Random.Range(0, (int)StatType.Size);
                currItem.secondaryStatValue = Random.Range(0, 5) * currentDungeonBestFloor;
                break;
            case ItemType.Consumable:
                currItem.name = consumeableRefs.names[rand.Next(consumeableRefs.names.Length)];
                currItem.fakeDescription = consumeableRefs.descriptions[rand.Next(consumeableRefs.descriptions.Length)];
                currItem.icon = consumeableRefs.sprites[rand.Next(consumeableRefs.sprites.Length)];
                currItem.primaryStatValue = Random.Range(1, 10) * currentDungeonBestFloor;
                currItem.spellType = (SpellType)Random.Range(1, (int)SpellType.Size);
                break;
        }

        return currItem;
    }
    
    //Returns the item to the user.
    public Item GetItemFromID(int id)
    {
        return items[id];
    }
}

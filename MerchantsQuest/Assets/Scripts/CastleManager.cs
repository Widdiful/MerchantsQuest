using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{

    public Transform lootPilePosition;

    public int maxLootPileSize;
    List<GameObject> lootPileRenderers;

    public static CastleManager instance;

    public List<Item> lootPile;

    private void Awake() 
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        lootPileRenderers = new List<GameObject>();
        lootPile = new List<Item>();
        for (int i = 0; i < maxLootPileSize; i++)
        {
            GameObject spriteHolder = new GameObject();
            spriteHolder.AddComponent<SpriteRenderer>();
            spriteHolder.transform.parent = lootPilePosition;
            spriteHolder.transform.position = lootPilePosition.position;
            lootPileRenderers.Add(spriteHolder);
        }
    }


    public void spawnPileOfLoot()
    {
        for (int i = 0; i < lootPile.Count; i++)
        {
            // spawn item? somehow  
            // set location to loot pile
            if(i< lootPileRenderers.Count)
            { 
                lootPileRenderers[i].GetComponent<SpriteRenderer>().sprite = lootPile[i].icon;
                // do some z rotation
                lootPileRenderers[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            }
            else
            {
                break;
            }
        }
    }
}

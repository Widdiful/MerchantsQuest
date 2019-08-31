using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public int minimumGoldInside, maximumGoldInside;
    public int chanceForMonsterInside;
    int goldInside;
    public bool wasBoobyTrapped;

    public Sprite openSprite;
    SpriteRenderer renderer;

    bool isClosed;
    // Start is called before the first frame update
    void Start()
    {
        wasBoobyTrapped = false;
        isClosed = true;
        goldInside = Random.Range(minimumGoldInside,maximumGoldInside);
        renderer = GetComponent<SpriteRenderer>();
        if(Random.Range(0,100) > chanceForMonsterInside)
        {
            wasBoobyTrapped = true;
        }
    }

// I know this isn't a great way to handle this but it functions
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isClosed)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if(Input.GetKeyDown("e"))
                {
                    openChest();
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(isClosed)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if(Input.GetKeyDown("e"))
                {
                    openChest();
                }
            }
        }
    }

    public void openChest()
    {
        if(wasBoobyTrapped)
        {
            if(CombatManager.instance)
                CombatManager.instance.StartCombat();
        }
        if(PartyManager.instance)
            PartyManager.instance.gold += goldInside;

        renderer.sprite = openSprite;
        isClosed = false;
        MessageBox.instance.NewMessage(string.Format("Found {0} coins.", goldInside));
        //Destroy(this.gameObject);

    }
}

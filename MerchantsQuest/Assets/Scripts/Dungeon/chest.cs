using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    public int goldInside;
    public bool wasBoobyTrapped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        
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

        Destroy(this.gameObject);

    }
}

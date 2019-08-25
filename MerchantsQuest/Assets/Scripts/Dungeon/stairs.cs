using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairs : MonoBehaviour
{
    public dungeonGeneration source;

    public bool tempTrigger;
    // Start is called before the first frame update
    void Start()
    {
        tempTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(tempTrigger)
        {
            source.incrementFloorCount();
            tempTrigger = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            source.incrementFloorCount();
        }
    }
}

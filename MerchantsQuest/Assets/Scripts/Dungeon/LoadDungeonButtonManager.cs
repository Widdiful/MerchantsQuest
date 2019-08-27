using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDungeonButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startAgain()
    {
        dungeonGeneration reference = FindObjectOfType<dungeonGeneration>();
        reference.setFloorNumber(0);
        reference.generateMap();
    }

    public void continueDungeon()
    {
        dungeonGeneration reference = FindObjectOfType<dungeonGeneration>();
        //dungeonGeneration.setFloorNumber(0);
        reference.generateMap();
    }
}

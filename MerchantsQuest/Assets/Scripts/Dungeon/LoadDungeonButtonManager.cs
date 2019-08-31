using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDungeonButtonManager : MonoBehaviour
{

    public void startAgain()
    {
        dungeonGeneration reference = FindObjectOfType<dungeonGeneration>();
        reference.setFloorNumber(0);
        reference.generateMap();

        AudioManager.Instance.TransitionToDungeonBGM();
    }

    public void continueDungeon()
    {
        dungeonGeneration reference = FindObjectOfType<dungeonGeneration>();
        //dungeonGeneration.setFloorNumber(0);
        reference.generateMap();

        AudioManager.Instance.TransitionToDungeonBGM();
    }
}

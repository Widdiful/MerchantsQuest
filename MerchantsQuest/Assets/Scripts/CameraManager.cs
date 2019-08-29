using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera townCamera, playerCam, transCam;
    public Camera cinemachineBrain, transitionCam;

    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTransitionCam()
    {
        if(townCamera.Priority == 11)
        {
            transCam.Follow = townCamera.gameObject.transform;
        }
        else if (playerCam.Priority == 11)
        {
            transCam.Follow = playerCam.gameObject.transform;
        }
        transCam.Priority = 12;

    }
    public void HideTransitionCam()
    {
        transCam.Priority = 9;
    }

    public void SetPlayerCamActive()
    {
        playerCam.Priority = 11;
        townCamera.Priority = 10;

    }

    public void SetTownCamActive()
    {
        townCamera.Priority = 11;
        playerCam.Priority = 10;
    }
}


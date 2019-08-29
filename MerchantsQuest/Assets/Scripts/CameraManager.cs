using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera townCamera, playerCam;
    public Camera cinemachineBrain, transitionCam;

    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTransitionCam()
    {
        transitionCam.gameObject.SetActive(true);
        cinemachineBrain.gameObject.SetActive(false);
    }
    public void HideTransitionCam()
    {
        transitionCam.gameObject.SetActive(false);
        cinemachineBrain.gameObject.SetActive(true);
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


﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera townCamera, playerCam;

    public static CameraManager instance;

    private void Awake()
    {
        instance = this;
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


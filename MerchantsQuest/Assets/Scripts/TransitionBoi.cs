﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionBoi : MonoBehaviour
{
    public Image image;
    float amountToTransition = 1.1f;
    float dissolveVal = 0.0f;
    public Texture2D[] dissolveTextures;
    public Texture2D[] combatTextures;
    public bool textureShown, textureHidden;

    [ContextMenu("Fade Out")]
    public void BeginHide()
    {
        dissolveVal = 0.0f;
        
        StartCoroutine(HideTexture());
    }


    IEnumerator HideTexture()
    {
        float transitionTick = 0.01f;

        if(GameManager.instance)
            transitionTick = (amountToTransition / GameManager.instance.transitionTime) *0.01f;

        while (dissolveVal <= amountToTransition)
        {
            dissolveVal += transitionTick;
            image.material.SetFloat("_DissolveAmount", dissolveVal);
            yield return null;
        }

        textureHidden = true;

    }

    public void CombatShowTexture()
    {
        textureHidden = false;
        textureShown = false;
        dissolveVal = 1.1f;
        image.material.SetTexture("_NoiseTex", combatTextures[Random.Range(0, dissolveTextures.Length)]);
        StartCoroutine(ShowTexture());
    }

    [ContextMenu("Fade In")]
    public void BeginShow()
    {
        textureHidden = false;
        textureShown = false;
        dissolveVal = 1.1f;
        image.material.SetTexture("_NoiseTex", dissolveTextures[Random.Range(0, dissolveTextures.Length)]);
        StartCoroutine(ShowTexture());
    }

    IEnumerator ShowTexture()
    {
        float transitionTick = 0.01f;
        if (GameManager.instance)
        {
            transitionTick = (amountToTransition / GameManager.instance.transitionTime) * 0.01f;
        }

        while (dissolveVal > -0.1f)
        {
            dissolveVal -= transitionTick;
            image.material.SetFloat("_DissolveAmount", dissolveVal);
            yield return null;
        }

        textureShown = true;
    }
}

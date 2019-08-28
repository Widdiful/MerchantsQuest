using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBoi : MonoBehaviour
{
    public MeshRenderer mesh;
    float amountToTransition = 1.1f;
    float dissolveVal = 0.0f;
    public Texture2D[] dissolveTextures;


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

        while(dissolveVal <= amountToTransition)
        {
            dissolveVal += transitionTick;
            mesh.material.SetFloat("_DissolveAmount", dissolveVal);
            yield return null;
        }

    }

    [ContextMenu("Fade In")]
    public void BeginShow()
    {
        dissolveVal = 1.1f;
        mesh.material.SetTexture("_NoiseTex", dissolveTextures[Random.Range(0, dissolveTextures.Length)]);
        StartCoroutine(ShowTexture());
    }

    IEnumerator ShowTexture()
    {
        float transitionTick = 0.01f;
        if (GameManager.instance)
        {
            transitionTick = (amountToTransition / GameManager.instance.transitionTime) * 0.01f;
        }

        while (dissolveVal > 0)
        {
            dissolveVal -= transitionTick;
            mesh.material.SetFloat("_DissolveAmount", dissolveVal);
            yield return null;
        }
    }
}

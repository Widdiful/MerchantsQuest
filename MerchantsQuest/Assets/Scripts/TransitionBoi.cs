using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBoi : MonoBehaviour
{
    public MeshRenderer mesh;
    float amountToTransition = 1.1f;
    float dissolveVal = 0.0f;


    [ContextMenu("Fade Out")]
    public void BeginFadeOut()
    {
        dissolveVal = 0.0f;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float transitionTick = 0.01f;

        if(GameManager.instance)
            transitionTick = amountToTransition / GameManager.instance.transitionTime;

        while(dissolveVal <= amountToTransition)
        {
            dissolveVal += transitionTick;
            mesh.material.SetFloat("_DissolveAmount", dissolveVal);
            yield return null;
        }

    }

    [ContextMenu("Fade In")]
    public void BeginFadeIn()
    {
        dissolveVal = 1.1f;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float transitionTick = 0.01f;
        if (GameManager.instance)
        {
            transitionTick = amountToTransition / GameManager.instance.transitionTime;
        }

        while (dissolveVal > 0)
        {
            dissolveVal -= transitionTick;
            mesh.material.SetFloat("_DissolveAmount", dissolveVal);
            yield return null;
        }
    }
}

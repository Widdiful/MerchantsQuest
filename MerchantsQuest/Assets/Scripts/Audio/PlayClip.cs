using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClip : MonoBehaviour
{
    public void UI()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.uiConfirm);
    }

    public void Attack()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.attackClips[Random.Range(0, AudioManager.Instance.attackClips.Length)]);
    }

    public void Defend()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.blockAtt);
    }
}

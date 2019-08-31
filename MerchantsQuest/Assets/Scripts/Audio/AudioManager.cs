﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    public AudioSource townBGM, combatBGM, dungeonBGM, overworldBGM, bossBGM, victoryBGM, sfxSource;

    public AudioClip attackClips;
    public AudioClip damageTaken, castSpell, damageSpell, healSpell, flee, blockAtt, levelUp, combatFin;
    public AudioClip enterLoc, downStairs, buyItem, sellItem, openShop;
    public AudioClip uiConfirm, collision;
    public AudioClip gameOver, death;

    public static AudioManager Instance;

    public float fadeSpeed;

    [Header("UI")]
    public Slider volSlider;
    public Image audioIconHolder;
    public Sprite noVol, lowVol, midVol, maxVol;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(AudioClip clip, float vol = 1)
    {
        sfxSource.PlayOneShot(clip, vol);
    }


#region BGM music transitions
    public void TransitionToTownBGM()
    {
        if(combatBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(townBGM, combatBGM));
        }
        else if(dungeonBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(townBGM, dungeonBGM));
        }
        else if(overworldBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(townBGM, overworldBGM));
        }
        else if(bossBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(townBGM, bossBGM));
        }
        else
        {
            StartCoroutine(StartAudio(overworldBGM));
        }
    }

    public void TransitionToCombatBGM()
    {
        if(dungeonBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(combatBGM, dungeonBGM));
        }
        else if(townBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(combatBGM, townBGM));
        }
        else if(overworldBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(combatBGM, overworldBGM));
        }
        else if(bossBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(combatBGM, bossBGM));
        }
        else
        {
            StartCoroutine(StartAudio(overworldBGM));
        }
    }

    public void TransitionToDungeonBGM()
    {
        if(townBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(dungeonBGM, townBGM));
        }
        else if(combatBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(dungeonBGM, combatBGM));
        }
        else if(overworldBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(dungeonBGM, overworldBGM));
        }
        else if(bossBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(dungeonBGM, bossBGM));
        }
        else
        {
            StartCoroutine(StartAudio(dungeonBGM));
        }
    }

    public void TransitionToOverworldBGM()
    {
        if (townBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(overworldBGM, townBGM));
        }
        else if (combatBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(overworldBGM, combatBGM));
        }
        else if (dungeonBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(overworldBGM, dungeonBGM));
        }
        else if(bossBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(overworldBGM, bossBGM));
        }
        else
        {
            StartCoroutine(StartAudio(overworldBGM));
        }
    }

    public void TransitionToBossBGM()
    {
        if (townBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(bossBGM, townBGM));
        }
        else if (combatBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(bossBGM, combatBGM));
        }
        else if (dungeonBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(bossBGM, dungeonBGM));
        }
        else if(overworldBGM.volume > 0)
        {
            StartCoroutine(FadeToAudio(bossBGM, overworldBGM));
        }
        else
        {
            StartCoroutine(StartAudio(bossBGM));
        }
    }


    IEnumerator FadeToAudio(AudioSource makeLouder, AudioSource makeQuieter)
    {
        makeLouder.Play();
        while (makeLouder.volume < 1.0f)
        {
            makeLouder.volume += Time.deltaTime * fadeSpeed;
            makeQuieter.volume -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        makeQuieter.Stop();
    }

    IEnumerator StartAudio(AudioSource sourceToStart)
    {
        sourceToStart.Play();
        while(sourceToStart.volume < 1.0f)
        {
            sourceToStart.volume += Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }


    public void SilenceAllBGM()
    {
        if(combatBGM.volume > 0.0f)
            StartCoroutine(SilenceAudio(combatBGM));
        if (townBGM.volume > 0.0f)
            StartCoroutine(SilenceAudio(townBGM));
        if (dungeonBGM.volume > 0.0f)
            StartCoroutine(SilenceAudio(dungeonBGM));
        if (bossBGM.volume > 0.0f)
            StartCoroutine(SilenceAudio(bossBGM));
        if (overworldBGM.volume > 0.0f)
            StartCoroutine(SilenceAudio(overworldBGM));
    }

    IEnumerator SilenceAudio(AudioSource sourceToSilence)
    {
        while(sourceToSilence.volume > 0.0f)
        {
            sourceToSilence.volume -= Time.deltaTime * fadeSpeed;
            yield return null;
        }

        sourceToSilence.Stop();


    }
    #endregion

#region mixer Adjustments
    
    public void AdjustVolume()
    {
        mixer.SetFloat("_Volume", Mathf.Log10(volSlider.value) * 20);

        if(volSlider.value == 0)
        {
            audioIconHolder.sprite = noVol;
        }
        else if(volSlider.value < 0.33f)
        {
            audioIconHolder.sprite = lowVol;
        }
        else if(volSlider.value < 0.66f)
        {
            audioIconHolder.sprite = midVol;
        }
        else
        {
            audioIconHolder.sprite = maxVol;
        }
    }
#endregion
}

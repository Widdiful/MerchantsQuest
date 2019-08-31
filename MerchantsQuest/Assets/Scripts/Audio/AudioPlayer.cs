using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void UIConfirm()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.uiConfirm);
    }

    public void OpenShop()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.openShop);
    }

    public void Attack()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.attackClips);
    }

    public void Defend()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.blockAtt);
    }

    public void SellItem()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.sellItem);
    }

    public void BuyItem()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.buyItem);
    }

    public void EnterStairs()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.enterLoc);
    }

    public void Flee()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.flee);
    }

    public void CastSpell()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.castSpell);
    }

    public void DamageSpell()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.damageSpell);
    }

    public void HealSpell()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.healSpell);
    }

    public void LevelUp()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.levelUp);
    }

    public void Block()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.blockAtt);
    }

    public void EnterLocation()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.enterLoc);
    }

    public void VictoryFanfare()
    {
        AudioManager.Instance.PlayClip(AudioManager.Instance.combatFin);
    }

    public void TakeDamage() {
        AudioManager.Instance.PlayClip(AudioManager.Instance.damageTaken);
    }
}

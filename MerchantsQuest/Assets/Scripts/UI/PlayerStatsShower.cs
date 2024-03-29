﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsShower : MonoBehaviour
{
    public TMP_Text levelText, hpText, attackText, defText, agiText, intText, goldText;

    PlayerStats stats;

    public void UpdateStats()
    {
        stats = PartyManager.instance.partyMembers[0];
        stats.SetCurrentHPMP();
        levelText.text = stats.level.ToString();
        hpText.text = stats.currentHP.ToString() + "/" + stats.maxHP;
        attackText.text = stats.currentATK.ToString();
        defText.text = stats.currentDEF.ToString();
        agiText.text = stats.currentAGI.ToString();
        intText.text = stats.currentINT.ToString();
        goldText.text = PartyManager.instance.gold.ToString();
    }
}

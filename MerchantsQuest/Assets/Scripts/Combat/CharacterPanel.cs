using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public PlayerStats stats;
    public Image turnIndicator;
    public TextMeshProUGUI name, level, hp, mp;

    public void UpdateStats(PlayerStats player) {
        stats = player;
        name.text = player.characterName;
        UpdateStats();
    }

    public void UpdateStats() {
        level.text = stats.level.ToString();
        hp.text = stats.currentHP + "\n/" + stats.maxHP;
        mp.text = stats.currentMP + "\n/" + stats.maxMP;
    }

    public void ToggleIndicator() {
        turnIndicator.enabled = !turnIndicator.enabled;
    }
}

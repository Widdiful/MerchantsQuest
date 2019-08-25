using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public PlayerStats stats;
    public Image turnIndicator;
    public Text name, hp, mp;

    public void UpdateStats(PlayerStats player) {
        stats = player;
        name.text = player.characterName;
        UpdateStats();
    }

    public void UpdateStats() {
        hp.text = stats.currentHP + "/" + stats.maxHP;
        mp.text = stats.currentMP + "/" + stats.maxMP;
    }

    public void ToggleIndicator() {
        turnIndicator.enabled = !turnIndicator.enabled;
    }
}

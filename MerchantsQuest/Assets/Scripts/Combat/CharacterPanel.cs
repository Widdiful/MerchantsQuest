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
        name.text = player.characterName;
        hp.text = player.currentHP + "/" + player.maxHP;
        mp.text = player.currentMP + "/" + player.maxMP;
    }

    public void ToggleIndicator() {
        turnIndicator.enabled = !turnIndicator.enabled;
    }
}

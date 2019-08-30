using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsMenuItem : MonoBehaviour
{
    public TextMeshProUGUI nameText, levelText, hpText, mpText, atkText, defText, agiText, intText;

    public void SetValues(PlayerStats player) {
        player.InitialiseCharacter();
        string questionMark = "";
        if ((player.weapon.id != 0 && !player.weapon.appraised) || (player.armour.id != 0 && !player.armour.appraised)) {
            questionMark = "?";
        }

        PlayerStats.BonusStats stats = player.GetStatData();
        nameText.text = player.characterName;
        levelText.text = string.Format("LV :{0}", player.level);
        hpText.text = string.Format("HP:{0}/{1}{2}", player.currentHP, stats.bonusHP, questionMark);
        mpText.text = string.Format("MP:{0}/{1}{2}", player.currentMP, stats.bonusMP, questionMark);
        atkText.text = string.Format("ATK:{0}{1}", stats.bonusATK, questionMark);
        defText.text = string.Format("DEF:{0}{1}", stats.bonusDEF, questionMark);
        agiText.text = string.Format("AGI:{0}{1}", stats.bonusAGI, questionMark);
        intText.text = string.Format("INT:{0}{1}", stats.bonusINT, questionMark);
    }
}

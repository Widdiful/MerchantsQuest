using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StatShower : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text typeText;
    public TMP_Text priceText;
    public RectTransform rectTransform;

    
    public void SetStats(Item item, RectTransform pos, int cost = 0)
    {
        rectTransform.position = pos.position;
        //Stop the panel going off the screen
        if (pos.position.x > (Screen.width * .5f))
        {
            rectTransform.position -= new Vector3(rectTransform.rect.size.x + pos.rect.size.x, 0, 0);
        }
        else
        {
            rectTransform.position += new Vector3(rectTransform.rect.size.x + pos.rect.size.x, 0, 0);
        }
        nameText.text = item.name;
        if (item.appraised)
        {
            if(item.primaryStat == StatType.None)
            {
                descriptionText.text = item.spellType.ToString() + ": " + item.primaryStatValue;
            }
            else
                descriptionText.text = item.primaryStat.ToString() + ": " + item.primaryStatValue;
        }
        else
        {
            descriptionText.text = item.fakeDescription;
        }

        typeText.text = item.type.ToString();
        priceText.text = cost.ToString();
    }
    
    public void SetSpellStats(Spell spell, RectTransform pos)
    {
        rectTransform.position = pos.position;
        if(pos.position.x > (Screen.width * .5f))
        {
            rectTransform.position -= new Vector3(rectTransform.rect.size.x + pos.rect.size.x, 0, 0);
        }
        else
        {
            rectTransform.position += new Vector3(rectTransform.rect.size.x + pos.rect.size.x, 0, 0);
        }

        if (spell.appraised)
        {
            nameText.text = spell.name;
            descriptionText.text = "Mana: " + spell.manaCost.ToString();
            typeText.text = spell.spellType.ToString() + ": " + spell.primaryStatValue.ToString();
        }
    }


}

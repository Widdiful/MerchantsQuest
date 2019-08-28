using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StatShower : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text typeText;
    public RectTransform rectTransform;

    
    public void SetStats(Item item, Vector2 pos)
    {
        rectTransform.position = pos;
        //Stop the panel going off the screen
        if (pos.x > (Screen.width * .5f))
        {
            rectTransform.position -= new Vector3(rectTransform.rect.size.x, 0, 0);
        }
        else
        {
            rectTransform.position += new Vector3(rectTransform.rect.size.x, 0, 0);
        }
        nameText.text = item.name;
        if (item.appraised)
        {
            descriptionText.text = item.primaryStat.ToString();
        }
        else
        {
            descriptionText.text = item.fakeDescription;
        }

        typeText.text = item.type.ToString();
    }
    
    public void SetSpellStats(Spell spell, Vector2 pos)
    {
        rectTransform.position = pos;
        if(pos.x > (Screen.width * .5f))
        {
            rectTransform.position -= new Vector3(rectTransform.rect.size.x, 0, 0);
        }
        else
        {
            rectTransform.position += new Vector3(rectTransform.rect.size.x, 0, 0);
        }
        nameText.text = spell.name;
        descriptionText.text = spell.manaCost.ToString();
        typeText.text = spell.primaryStatValue.ToString();
    }

}

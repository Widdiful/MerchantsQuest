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
        rectTransform.position += new Vector3(rectTransform.rect.size.x, 0, 0);
        nameText.text = item.name;
        if (item.appraised)
            descriptionText.text = item.realStat.ToString();
        else
            descriptionText.text = item.fakeDescription;

        typeText.text = item.type.ToString();
    }

}

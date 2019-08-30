using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldShower : MonoBehaviour
{
    public TMP_Text goldText;
    int currGold;

    private void Start()
    {
        UpdateGold();
    }

    private void LateUpdate()
    {
        if (currGold != PartyManager.instance.gold)
            UpdateGold();
    }
    void UpdateGold()
    {
        currGold = PartyManager.instance.gold;
        goldText.text = currGold.ToString();
    }


}

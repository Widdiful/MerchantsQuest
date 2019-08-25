using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    public List<PlayerStats> partyMembers = new List<PlayerStats>();

    public static PartyManager instance;

    private void Awake() {
        instance = this;
    }
}

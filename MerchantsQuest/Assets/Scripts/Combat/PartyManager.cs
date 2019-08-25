using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<PlayerStats> partyMembers = new List<PlayerStats>();

    public static PartyManager instance;

    private void Awake() {
        instance = this;
    }
}

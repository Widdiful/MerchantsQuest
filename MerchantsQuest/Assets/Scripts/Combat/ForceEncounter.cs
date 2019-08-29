﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceEncounter : MonoBehaviour
{
    public List<EnemyStats> enemies = new List<EnemyStats>();

    private void OnTriggerEnter2D(Collider2D collision) {
        GameManager.instance.StartCombat(enemies);
    }
}
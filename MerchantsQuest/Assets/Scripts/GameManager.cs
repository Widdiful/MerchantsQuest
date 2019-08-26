using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject combatRoot, overworldRoot;
    public CombatManager combatManager;

    public static GameManager instance;

    private void Awake() {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void StartCombat() {
        combatRoot.SetActive(true);
        overworldRoot.SetActive(false);
        combatManager.StartCombat();
    }

    public void EndCombat() {
        combatRoot.SetActive(false);
        overworldRoot.SetActive(true);
    }

    private void Update() {
        if (Input.GetKeyDown("i")) {
            StartCombat();
        }
    }
}

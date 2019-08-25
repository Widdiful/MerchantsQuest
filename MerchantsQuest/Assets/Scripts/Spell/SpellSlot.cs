using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public Spell spellInSlot;
    public bool empty;
    public Image icon;

    private void Awake()
    {
        if (icon.sprite == null)
            icon.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        empty = true;
    }

    public void SetSpell(Spell spell)
    {
        spellInSlot = spell;
        empty = false;
        icon.color = Color.white;
        icon.sprite = spellInSlot.icon;
    }

    public Spell GetSpell()
    {
        return spellInSlot;
    }

    public void RemoveSpell()
    {
        icon.sprite = null;
        icon.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        spellInSlot = new Spell();
        empty = true;
    }

    public void EquipSpell()
    {

    }
}

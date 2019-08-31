using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlayerStats : StatsBase
{
    public Item weapon, armour;

    public class BonusStats {
        public int bonusHP, bonusMP, bonusATK, bonusDEF, bonusAGI, bonusINT = 0;
    }

    public override int TakeDamage(int damage, bool ignoreDefence) {
        if (armour.id != 0 && !armour.appraised)
            InventoryManager.Instance.AppraiseItem(armour.id);
        return base.TakeDamage(damage, ignoreDefence);
    }

    public override void Kill() {
        base.Kill();
        AudioPlayer.Instance.Death();
        CombatManager.instance.playerTeam.Remove(this);
    }

    public override void Attack(StatsBase target, int damage) {
        base.Attack(target, damage);
        if (weapon.id != 0 && !weapon.appraised)
            InventoryManager.Instance.AppraiseItem(weapon.id);
    }

    public override void GetCommand() {
        base.GetCommand();

        CombatManager.instance.EnableCommandCanvas();
    }

    public override bool InitialiseCharacter() {
        base.InitialiseCharacter();

        BonusStats bonus = new BonusStats();
        CalculateBonusStats(weapon, bonus);
        CalculateBonusStats(armour, bonus);

        maxHP = baseHP + (hpPerLevel * (level - 1)) + bonus.bonusHP;
        maxMP = baseMP + (mpPerLevel * (level - 1)) + bonus.bonusMP;
        currentATK = baseATK + (atkPerLevel * (level - 1)) + bonus.bonusATK;
        currentDEF = baseDEF + (defPerLevel * (level - 1)) + bonus.bonusDEF;
        currentAGI = baseAGI + (agiPerLevel * (level - 1)) + bonus.bonusAGI;
        currentINT = baseINT + (intPerLevel * (level - 1)) + bonus.bonusINT;

        return true;
    }

    public BonusStats GetStatData() {
        BonusStats result = new BonusStats();

        result.bonusHP = baseHP + (hpPerLevel * (level - 1));
        result.bonusMP = baseMP + (mpPerLevel * (level - 1));
        result.bonusATK = baseATK + (atkPerLevel * (level - 1));
        result.bonusDEF = baseDEF + (defPerLevel * (level - 1));
        result.bonusAGI = baseAGI + (agiPerLevel * (level - 1));
        result.bonusINT = baseINT + (intPerLevel * (level - 1));

        if (weapon.appraised) {
            CalculateBonusStats(weapon, result);
        }
        if (armour.appraised) {
            CalculateBonusStats(armour, result);
        }
        
        return result;
    }

    private void CalculateBonusStats(Item item, BonusStats bonus) {
        switch (item.primaryStat) {
            case StatType.Health:
                bonus.bonusHP += item.primaryStatValue;
                break;
            case StatType.Magic:
                bonus.bonusMP += item.primaryStatValue;
                break;
            case StatType.Attack:
                bonus.bonusATK += item.primaryStatValue;
                break;
            case StatType.Defence:
                bonus.bonusDEF += item.primaryStatValue;
                break;
            case StatType.Agility:
                bonus.bonusAGI += item.primaryStatValue;
                break;
            case StatType.Intelligence:
                bonus.bonusINT += item.primaryStatValue;
                break;
        }

        switch (item.secondaryStat) {
            case StatType.Health:
                bonus.bonusHP += item.secondaryStatValue;
                break;
            case StatType.Magic:
                bonus.bonusMP += item.secondaryStatValue;
                break;
            case StatType.Attack:
                bonus.bonusATK += item.secondaryStatValue;
                break;
            case StatType.Defence:
                bonus.bonusDEF += item.secondaryStatValue;
                break;
            case StatType.Agility:
                bonus.bonusAGI += item.secondaryStatValue;
                break;
            case StatType.Intelligence:
                bonus.bonusINT += item.secondaryStatValue;
                break;
        }
    }
}

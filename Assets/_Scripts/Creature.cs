using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Weapon equippedWeapon;
    public Armor equippedArmor;

    public int totalHitPoints = 100;
    public int currentHitPoints = 100;

    public delegate void EquipTrigger();
    public EquipTrigger onEquipTriggered;

    public delegate void AttackTrigger();
    public AttackTrigger onAttackTriggered;

    public delegate void DamageEnemyTrigger(Creature targetCreature, int damageDealt);
    public DamageEnemyTrigger onDamageEnemyTriggered;

    public delegate void DamageSelfTrigger(Creature sourceCreature, int damageDealt);
    public DamageSelfTrigger onDamageSelfTriggered;

    public void Equip(Equipment newEquipment)
    {
        switch (newEquipment.equipmentType)
        {
            case EquipmentType.Weapon:
                break;
            case EquipmentType.Armor:
                break;
            default:
                Debug.LogError("Invalid EquipmentType: " + newEquipment.equipmentType + " Unable to equip.");
                break;
        }
    }

    private void DropCurrentWeapon()
    {
        //Logic to drop it into the world
        this.equippedWeapon.Unequip();

    }

    private void EquipWeapon(Weapon newWeapon)
    {
        this.DropCurrentWeapon();
        this.equippedWeapon = newWeapon;
        this.equippedWeapon.Equip(this);        
    }

    public void TriggerEquip()
    {
        if (this.onEquipTriggered != null)
        {
            this.onEquipTriggered();
        }
    }

    public void TriggerAttack()
    {
        if (this.onAttackTriggered != null)
        {
            this.onAttackTriggered();
        }
    }

    public void TriggerDamageEnemy(Creature targetCreature, int damageDealt)
    {
        if (this.onDamageEnemyTriggered != null)
        {
            this.onDamageEnemyTriggered(targetCreature, damageDealt);
        }
    }

    public void TriggerDamageSelf(Creature sourceCreature, int damageDealt)
    {
        if (this.onDamageSelfTriggered != null)
        {
            this.onDamageSelfTriggered(sourceCreature, damageDealt);
        }
    }
}

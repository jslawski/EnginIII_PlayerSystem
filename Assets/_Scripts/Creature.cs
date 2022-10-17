using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public Weapon equippedWeapon;
    public Armor equippedArmor;

    public int totalHitPoints = 100;
    public int currentHitPoints = 100;

    [HideInInspector]
    public AttackZone attackZone;

    public delegate void EquipTrigger();
    public EquipTrigger onEquipTriggered;

    public delegate void AttackTrigger();
    public AttackTrigger onAttackTriggered;

    public delegate void DamageEnemyTrigger(Creature targetCreature, int damageDealt);
    public DamageEnemyTrigger onDamageEnemyTriggered;

    public delegate void DamageSelfTrigger(Creature sourceCreature, int damageDealt);
    public DamageSelfTrigger onDamageSelfTriggered;

    public float moveSpeed = 5f;

    private Weapon unarmedWeapon;    
    private Armor nakedArmor;
    
    private void Start()
    {
        this.unarmedWeapon = Resources.Load<Weapon>("Equipment/Weapons/Unarmed");
        this.nakedArmor = Resources.Load<Armor>("Equipment/Armor/Naked");

        this.attackZone = GetComponentInChildren<AttackZone>(true);

        if (this.equippedWeapon != null)
        {
            this.equippedWeapon.Equip(this);
            this.TriggerEquip();
        }
        if (this.equippedArmor != null)
        {
            this.equippedArmor.Equip(this);
            this.TriggerEquip();
        }
    }

    public void DropWeapon()
    {
        if (this.equippedWeapon == this.unarmedWeapon)
        {
            return;
        }

        GameObject grabbableWeaponPrefab = Resources.Load<GameObject>("Prefabs/GrabbableWeapon");
        GameObject grabbableWeaponObject = Instantiate(grabbableWeaponPrefab, this.gameObject.transform.position, new Quaternion());
        grabbableWeaponObject.GetComponent<GrabbableWeapon>().Setup(this.equippedWeapon);
        this.equippedWeapon.Unequip();

        this.equippedWeapon = this.unarmedWeapon;
        this.equippedWeapon.Equip(this);
    }

    public void DropArmor()
    {
        if (this.equippedArmor == this.nakedArmor)
        {
            return;
        }

        GameObject grabbableArmorPrefab = Resources.Load<GameObject>("Prefabs/GrabbableArmor");
        GameObject grabbableArmorObject = Instantiate(grabbableArmorPrefab, this.gameObject.transform.position, new Quaternion());
        grabbableArmorObject.GetComponent<GrabbableArmor>().Setup(this.equippedArmor);
        this.equippedArmor.Unequip();

        this.equippedArmor = this.nakedArmor;
        this.equippedArmor.Equip(this);
    }

    public void Equip(Weapon newWeapon)
    {
        this.DropWeapon();
        this.equippedWeapon = newWeapon;
        this.equippedWeapon.Equip(this);

        this.attackZone.transform.localScale = 
            new Vector3(this.equippedWeapon.attackZoneDimensions.x, 
            this.equippedWeapon.attackZoneDimensions.y, 1.0f);

        this.TriggerEquip();
    }

    private void Equip(Armor newArmor)
    {
        this.DropArmor();
        this.equippedArmor = newArmor;
        this.equippedArmor.Equip(this);
        this.TriggerEquip();
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

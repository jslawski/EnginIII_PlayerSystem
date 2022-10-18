using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Creature : MonoBehaviour
{
    [SerializeField]
    private Weapon startingWeapon;
    [SerializeField]
    private Armor startingArmor;
    
    public Weapon equippedWeapon;    
    public Armor equippedArmor;

    public int totalHitPoints = 100;
    public int currentHitPoints = 100;

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

    private Animator creatureAnimator;

    private TextMeshProUGUI hpText;

    [HideInInspector]
    public GrabbableWeapon weaponPickupCandidate;

    [HideInInspector]
    public LootChest lootChestCandidate;

    private void Start()
    {
        this.unarmedWeapon = Resources.Load<Weapon>("Equipment/Weapons/Unarmed");
        this.nakedArmor = Resources.Load<Armor>("Equipment/Armor/Naked");

        this.attackZone = GetComponentInChildren<AttackZone>(true);
        this.attackZone.Setup();

        this.creatureAnimator = GetComponent<Animator>();

        this.hpText = GetComponentInChildren<TextMeshProUGUI>();

        if (this.startingWeapon != null)
        {
            this.Equip(this.startingWeapon);
        }
        else
        {
            this.Equip(this.unarmedWeapon);
        }
        if (this.startingArmor != null)
        {
            this.Equip(this.startingArmor);
        }
        else
        {
            this.Equip(this.nakedArmor);
        }
    }

    private void Update()
    {
        this.hpText.text = ("HP: " + this.currentHitPoints);

        Vector3 adjustedPosition =
            new Vector3(this.gameObject.transform.position.x,
            this.gameObject.transform.position.y + 1.5f,
            -0.5f);

        this.hpText.transform.parent.transform.parent.position = adjustedPosition;
        this.hpText.transform.parent.transform.parent.rotation = Quaternion.identity;
    }

    public void Interact()
    {
        if (this.weaponPickupCandidate != null)
        {
            this.PickUpWeapon();
        }
        else if (this.lootChestCandidate != null)
        {
            this.OpenLootChest();
        }
    }

    public void PickUpWeapon()
    {
        this.Equip(this.weaponPickupCandidate.weaponDetails);
        Destroy(this.weaponPickupCandidate.gameObject);        
    }

    public void OpenLootChest()
    {
        this.lootChestCandidate.GenerateLoot();        
    }

    public void DropWeapon()
    {
        if (this.equippedWeapon == null || this.equippedWeapon == this.unarmedWeapon)
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
        if (this.equippedArmor == null || this.equippedArmor == this.nakedArmor)
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

        this.SetupAttackZone();

        this.TriggerEquip();
    }

    private void SetupAttackZone()
    {
        this.attackZone.gameObject.transform.localScale =
            new Vector3(this.equippedWeapon.attackZoneDimensions.y,
            this.equippedWeapon.attackZoneDimensions.x, 1.0f);

        float adjustedXPosition = (this.attackZone.gameObject.transform.localScale.x / 2.0f) + 
            (this.gameObject.transform.localScale.x / 2.0f);

        Vector3 adjustedPosition = new Vector3(adjustedXPosition,
            this.attackZone.gameObject.transform.localPosition.y,
            this.attackZone.gameObject.transform.localPosition.z);
        
        this.attackZone.gameObject.transform.localPosition = adjustedPosition;           
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

    public void TakeDamage(Creature attackingCreature, int damage)
    {
        this.currentHitPoints -= damage;
        this.creatureAnimator.SetTrigger("DamagedTrigger");

        this.TriggerDamageSelf(attackingCreature, damage);        
    }
}

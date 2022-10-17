using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private Creature owningCreature;
    private BoxCollider attackCollider;

    private void Start()
    {
        this.owningCreature = GetComponentInParent<Creature>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hittable")
        {
            this.ProcessHit(other.gameObject.GetComponent<Creature>());
        }
    }

    public void ProcessHit(Creature hitCreature)
    {
        if (hitCreature != null)
        {
            hitCreature.currentHitPoints -= this.owningCreature.equippedWeapon.attackPower;
            this.owningCreature.TriggerDamageEnemy(hitCreature, this.owningCreature.equippedWeapon.attackPower);
        }
    }
}

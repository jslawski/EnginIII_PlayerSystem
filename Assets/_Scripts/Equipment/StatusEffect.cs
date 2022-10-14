using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    private Creature ownerTarget;
    private Creature nonOwnerTarget;
    
    public void SetupStatusEffect(Creature owner, Creature nonOwner)
    {
        this.ownerTarget = owner;
        this.nonOwnerTarget = nonOwner;
    }

    public virtual void ApplyStatusEffect() { }
}

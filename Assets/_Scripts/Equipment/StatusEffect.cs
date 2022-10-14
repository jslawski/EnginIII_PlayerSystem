using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffect : MonoBehaviour
{
    protected Creature ownerTarget;
    protected Creature nonOwnerTarget;
    
    public void SetupStatusEffect(Creature owner, Creature nonOwner)
    {
        this.ownerTarget = owner;
        this.nonOwnerTarget = nonOwner;
    }

    public virtual void ApplyStatusEffect() { }
}

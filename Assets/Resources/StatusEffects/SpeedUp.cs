using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpeedUp : StatusEffect
{
    private float speedModifier = 5.0f;

    public override void ApplyStatusEffect()
    {
        this.ownerTarget.moveSpeed += this.speedModifier;
    }

    private void OnDestroy()
    {
        this.ownerTarget.moveSpeed -= this.speedModifier;
    }
}

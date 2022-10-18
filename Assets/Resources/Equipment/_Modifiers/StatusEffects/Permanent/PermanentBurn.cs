using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentBurn : StatusEffect
{
    private int fireDamage = 2;

    private float delayBetweenBurnsInSeconds = 1.0f;

    public override void ApplyStatusEffect()
    {
        StartCoroutine(this.ApplyBurn());
        base.ApplyStatusEffect();
    }

    private IEnumerator ApplyBurn()
    {
        while (true)
        {
            this.nonOwnerTarget.TakeDamage(this.ownerTarget, this.fireDamage);
            yield return new WaitForSeconds(this.delayBetweenBurnsInSeconds);
        }
    }
}

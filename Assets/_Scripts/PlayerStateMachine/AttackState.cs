using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState
{
    private float currentSwingTimeInSeconds = 0.0f;
    private float swingTimeInSeconds = 0.5f;

    public override void Enter(PlayerController controller)
    {
        base.Enter(controller);

        this.controller.creatureReference.attackZone.gameObject.SetActive(true);
        this.controller.creatureReference.TriggerAttack();
    }

    public override void Exit()
    {
        base.Exit();

        this.controller.creatureReference.attackZone.gameObject.SetActive(false);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (this.currentSwingTimeInSeconds < this.swingTimeInSeconds)
        {
            this.currentSwingTimeInSeconds += Time.fixedDeltaTime;
        }
        else
        {
            this.controller.ChangeState(new IdleState());
        }
    }
}

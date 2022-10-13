using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public override void Enter(PlayerController controller)
    {
        base.Enter(controller);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (this.PressedMoveKey() == true)
        {
            this.controller.ChangeState(new MoveState());
        }
        else if (this.PressedAttackKey())
        {
            this.controller.ChangeState(new AttackState());
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    private bool PressedMoveKey()
    {
        return (Input.GetKeyDown(this.controller.upKey) || Input.GetKeyDown(this.controller.downKey) ||
                Input.GetKeyDown(this.controller.leftKey) || Input.GetKeyDown(this.controller.rightKey));
    }    
}

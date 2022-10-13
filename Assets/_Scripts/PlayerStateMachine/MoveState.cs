using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    private float moveSpeed = 5f;
    Vector3 moveDirection = Vector3.zero;

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

        this.moveDirection = this.UpdateMoveDirection();

        if (this.StoppedHoldingMoveKeys())
        {
            this.controller.ChangeState(new IdleState());
        }
        else if (this.PressedAttackKey())
        {
            this.controller.ChangeState(new AttackState());
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        float finalMoveSpeed = this.moveSpeed * this.controller.moveSpeedModifier * Time.fixedDeltaTime;
        Vector3 targetDestination = this.controller.playerRb.position + (this.moveDirection * finalMoveSpeed);

        this.controller.playerRb.MovePosition(targetDestination);        
    }
    
    private bool StoppedHoldingMoveKeys()
    {
        return (!Input.GetKey(this.controller.upKey) && !Input.GetKey(this.controller.downKey) &&
                !Input.GetKey(this.controller.leftKey) && !Input.GetKey(this.controller.rightKey));
    }
    

    private Vector3 UpdateMoveDirection()
    {
        Vector3 currentMoveDirection = Vector3.zero;

        if (Input.GetKey(this.controller.upKey))
        {
            currentMoveDirection.y += 1.0f;
        }
        if (Input.GetKey(this.controller.downKey))
        {
            currentMoveDirection.y -= 1.0f;
        }
        if (Input.GetKey(this.controller.rightKey))
        {
            currentMoveDirection.x += 1.0f;
        }
        if (Input.GetKey(this.controller.leftKey))
        {
            currentMoveDirection.x -= 1.0f;
        }

        return currentMoveDirection.normalized;        
    }
}

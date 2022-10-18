using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerController controller;

    public virtual void Enter(PlayerController controller)
    {
        this.controller = controller;
    }
    public virtual void Exit() { }

    public virtual void UpdateState()
    {
        if (Input.GetKeyDown(this.controller.interactKey))
        {
            this.controller.creatureReference.Interact();
        }
    }


    public virtual void FixedUpdateState() { }

    protected bool PressedInteractKey()
    {
        return (Input.GetKeyDown(this.controller.interactKey));
    }

    protected bool PressedAttackKey()
    {
        return (Input.GetKeyDown(this.controller.attackKey));
    }
}

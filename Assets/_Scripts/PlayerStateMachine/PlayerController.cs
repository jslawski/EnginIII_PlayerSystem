using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private string stateName;

    public PlayerState currentState;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode attackKey = KeyCode.Space;
    public KeyCode interactKey = KeyCode.E;

    [HideInInspector]
    public Rigidbody playerRb;

    [HideInInspector]
    public float moveSpeedModifier = 1.0f;

    private void Start()
    {
        this.playerRb = GetComponent<Rigidbody>();

        this.ChangeState(new IdleState());
    }
    
    private void Update()
    {
        this.currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        this.currentState.FixedUpdateState();
    }

    public void ChangeState(PlayerState newState)
    {
        if (this.currentState != null)
        {
            this.currentState.Exit();
        }

        this.currentState = newState;
        this.currentState.Enter(this);

        this.stateName = this.currentState.GetType().ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{

    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 0.4f;

        player.SetVelocity(5 * -player.facingDir, player.jumpForce); // будет прыгать в противоположную сторону от стены


    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.airState);
        }

        if (player.whatIsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState); // когда приземлилась на землю после прыжка, перейди в состояние idleState
        }
    }
}

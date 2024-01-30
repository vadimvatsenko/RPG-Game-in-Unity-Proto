// 4й скрипт
using System.Diagnostics;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();


        if (player.WhatIsWallDecected())
        {
            stateMachine.ChangeState(player.idleState); // при движением персонажа, когда он врезался в стену прекрати анимацию бега

        }

        player.SetVelocity(xInput * player.moveSpeed, player.rb.velocity.y); // предает движение в скрипт Player

        if (xInput == 0) // если движение равно 0, поменяй состояние на спокойное
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

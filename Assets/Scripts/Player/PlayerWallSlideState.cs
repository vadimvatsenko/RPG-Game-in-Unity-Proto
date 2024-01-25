using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 10
public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if ((xInput != 0 && player.facingDir != xInput) || player.whatIsGroundDetected()) // если ввод управления не равен 0 и куда смотрит персонаж не равно вводу, перейди в состояние покоя или коснулся земли
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (yInput < 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // если нажата кнопка вниз, то нормальная скорость слайда
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.9f); // при скльжении скорость будет 90% от скорости
        }

    }
}

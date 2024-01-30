
using UnityEngine;
// 8й

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (player.WhatIsWallDecected()) // добавил дополнительную проверку, если нажатая кнопка совпадает с вводом, то прилипни к стене
        {
            stateMachine.ChangeState(player.wallSlideState);
        }


        if (player.whatIsGroundDetected()) // если игрок на земле
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (xInput != 0)
        {
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y); // это для того, что бы мы могли двигается в воздухе, по x мы замедляем скорость до 80 процентов и летим в направлении xInput, по y своё место положения
        }

    }
}

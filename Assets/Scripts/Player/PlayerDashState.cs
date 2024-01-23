using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//9
public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration; // теперь вместо 1,5 мы подставили переменную player.dashDuration
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y); // когда выходим из даша, нужно установить скорость по x - 0
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir, 0); // передаем в метод скорость даша * (на 1 или -1), по y поставили 0, чтобы игрок не стримился внуз во время даша

        if (stateTimer < 0) // когда таймер истёк перейди в обычное состояние
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

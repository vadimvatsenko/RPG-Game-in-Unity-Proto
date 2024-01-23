// 6й
using UnityEngine;

public class PlayerGroundedState : PlayerState // новое состояние, будет наследоватся от PlayerState, а PlayerIdleState и PlayerMoveState будут теперь наследоватся от этого скрипта
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (!player.whatIsGroundDetected()) // это решит проблему вызова бега в воздухе после даша
        {
            stateMachine.ChangeState(player.airState);
        }

        if (Input.GetKeyDown(KeyCode.Space) && player.whatIsGroundDetected())// player.whatIsGroundDetected() - проверяет на земле мы или нет
        {
            stateMachine.ChangeState(player.jumpState);
        }

    }
}

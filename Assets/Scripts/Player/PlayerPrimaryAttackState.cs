using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//12
public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        Debug.Log(triggerCalled);
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

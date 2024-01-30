using UnityEngine;
// 3й скрипт
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) // унаследован конструктор из PlayerState

    {
    }
    public override void Enter() // унаследован метод из PlayerState
    {
        base.Enter();

        rb.velocity = new Vector2(0, 0); // устанавливаем значения по 0 0, это предотвратит баг с постояной ходьбой, после прыжка со стены
    }

    public override void Exit() // унаследован метод из PlayerState 
    {
        base.Exit();
    }

    public override void Update() // унаследован метод из PlayerState
    {
        base.Update();

        if (xInput == player.facingDir && player.WhatIsWallDecected()) // если лицо смотрит в право равно кнопке движиния в право и обнаружена стена, то выйди и не выполняй код ниже
        {
            return; // во общем игрок не будет бежать, если прикоснулся стены
        }


        if (xInput != 0) // если координаты по xInput в PlayerState не равно 0, поменяй состояние на ходьбу
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}


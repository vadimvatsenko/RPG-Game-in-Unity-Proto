// 3й скрипт
public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName) // унаследован конструктор из PlayerState

    {
    }
    public override void Enter() // унаследован метод из PlayerState
    {
        base.Enter();
    }

    public override void Exit() // унаследован метод из PlayerState 
    {
        base.Exit();
    }

    public override void Update() // унаследован метод из PlayerState
    {
        base.Update();



        if (xInput != 0) // если координаты по xInput в PlayerState не равно 0, поменяй состояние на ходьбу
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}


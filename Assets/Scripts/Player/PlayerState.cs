using UnityEngine;
// 1й файл. Этот код представляет собой базовый класс PlayerState 
// для реализации состояний игрока в игровой машине состояний
public class PlayerState
{
    protected PlayerStateMachine stateMachine; // ссылка на скрипт PlayerStateMachine со своими методами

    protected Player player; // сслыка на скрипт Player со своими методами

    protected Rigidbody2D rb; // создаем защищённое поле Rigidbody2D

    private string animBoolName; // приватная переменная с названием анимации
    protected float xInput;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) // Конструктор класса PlayerState принимает и создает экземпляр

    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter() // Виртуальный метод для входа в состояние

    {
        player.anim.SetBool(animBoolName, true); // когда мы входим, то включаем анимацию
        rb = player.rb;
        // для того что бы сократили код в PlayerMoveState, там есть эта строка - player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y); - ранее rb.velocity.y был player.rb.velocity.y
        // так как наследовался со скрипта Player

    }

    public virtual void Update() // Виртуальный метод для обновления состояния

    {
        xInput = Input.GetAxis("Horizontal");

        player.anim.SetFloat("yVelocity", rb.velocity.y); // rb.velocity.y - текущее положение игрока по y
    }

    public virtual void Exit()  // Виртуальный метод для выхода из состояния

    {
        player.anim.SetBool(animBoolName, false); // когда мы выходим, то выключаем анимацию

    }
}


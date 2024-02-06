using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//12
public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter; // счётчик комбо
    private float lastTimeAttacked; // время на атаку
    private float comboWindow = 2;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow) // это сбросит счётчик, если он перевалил за 2ку или я начал двигаться, то счётчик обнулиться
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);

        #region Choose attack direction 
        float attackDir = player.facingDir; // условие втаки по направлению
        if (xInput != 0)
        {
            attackDir = xInput;
        }
        #endregion

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y); // это заставит игрока немного двигаться вперед при атаке

        stateTimer = 0.1f; // ставим таймер для плавности
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.15f); // запуск Coroutine public IEnumerable BusyFor(float _seconds)
        comboCounter++; // после атаки увеличиваем счётчик на единицу
        lastTimeAttacked = Time.time; // Time.time возвращает текущее время (в секундах) с момента начала выполнения приложения или загрузки текущей сцены
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            player.ZeroVelocity(); // это остановит игрока во время атаки
        }
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

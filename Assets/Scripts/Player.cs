using System;
using UnityEditor.Callbacks;
using UnityEngine;
public class Player : Entity
{
    [Header("Move info")]
    [SerializeField] private float moveSpeed = 5; // ускорение нашего игрока, так как xInput может быть -1 или 1, то это значение очень маленькое, потому нужно его умножать на ускорение
    [SerializeField] private float jumpForce = 5;

    [Header("Dash info")] // шавка в редакторе для рывка
    [SerializeField] private float dashSpeed; // скорость рывка
    [SerializeField] private float dashDuration; // продолжительность рывка
    private float dashTime; // время рывка
    [SerializeField] private float dashCooldown; //10 - время перерыва между рывками
    private float dashCooldownTimer; // таймер перерыва между рывками

    private float _xInput; // создали публичную переменную, в нее будет записыватся положение Horizontal
                           // private bool _isMoving; // переменная которая будет хранить в себе, двигается ли наш персонаж // удаляем эту переменную, так как она нам нужна будет только в методе AnimatorController()




    [Header("Attack info")]
    private float comboTimeWindow; // время которое будет уменьшатся таймером, если будет ниже 0ля, то начинай комбо с 1й атаки(заново)
    private float comboTime = 1f; // время, на атаку комбо, если это время вышло, то комбо начинается с первого, а не на том котором остановилась
    private bool isAttacking; // атакует ли на игрок
    private int comboCounter; // счётчик комбо

    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        Movement();
        CheckInput();


        dashTime -= Time.deltaTime; // время рывка будет уменьшатся каждый кадр
        dashCooldownTimer -= Time.deltaTime; // время перерыва рывка будет уменьшатся каждый кадр, изначально ничему не равен и сразу уходит в минус

        comboTimeWindow -= Time.deltaTime; // уменьшает время комбо, нужно успеть в это время, иначе комбо прекратится и нужно начинать заново


        AnimatorController();
        FlipController();
    }

    public void AttackOver() // метод, который отвечает за прекращение анимации атаки, мы будем добавлять этот метод на последний кадр анимации атаки
    {
        isAttacking = false;

        comboCounter++; // увеличиваем счётчик комбо на 1

        if (comboCounter > 2)
        { // сбрасываем счётчик комбо, если он выше 2
            comboCounter = 0;
        }

    }

    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal"); // записываем в переменную xInput нажатие клавиш A и D
        if (Input.GetKeyDown(KeyCode.Mouse0)) // если мы нажали кнопку мыши, то isAttacking = true
        {
            StartAttaceEvent();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) // добавили проверку, если нажали LShift 
        {
            DashAbility();
        }
    }

    private void StartAttaceEvent() // вынесли в отдельный метод
    {
        if (!_isGrounded) // если игрок не на земле, то не выполняй атаку
        {
            return;
        }
        if (comboTimeWindow < 0) // если время на комбоатаку вышло, то начинай с первой атаки
        {
            comboCounter = 0;
        }
        isAttacking = true;
        comboTimeWindow = comboTime; // во время атаки значение времени на комбоатаку записывается в comboTimeWindow, а в Update запускается обратный счётчик
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking) //dashCooldownTimer меньше нуля и не атакует, а он меньше, так как работает на нем таймер в обратную сторону
        {
            dashCooldownTimer = dashCooldown; // записалась 10 в таймер и он пошёл убавлятся выше в Update dashCooldownTimer -= Time.deltaTime
            dashTime = dashDuration; // когда мы наживаем LShift время рывка становится 2ка(из редактора) и запускается таймер, тот, что сверху
        }

    }
    private void Movement()
    {
        if (isAttacking) // если наш игрок атакует, то он стоит на месте
        {
            _rb.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        { // если время рывка больше 0, то выполни рывок
            _rb.velocity = new Vector2(_facingDir * dashSpeed, 0); //_xInput - направление(1 или -1) * на dashSpeed - скорость рывка, а по y - 0, что бы не терял высоту при рывке в воздухе
        }
        else
        {
            _rb.velocity = new Vector2(_xInput * moveSpeed, _rb.velocity.y); // xInput(1 или -1) * на ускорение, по y равно самому себе
        }

    }
    private void Jump()
    {
        if (_isGrounded) _rb.velocity = new Vector2(_rb.velocity.x, jumpForce); // реализован прыжок, по х собственное положение, по y - jumpForce // условие, если на земле выполни прыжок
    }
    private void AnimatorController()
    {
        bool _isMoving = Math.Abs(_rb.velocity.x) > 0f; // _rb.velocity.x - может быть как положительным, так и отрицательным, потому мы к нему применяем Math.Abs - он конвертирует любое число в положительное
        //bool _isMoving = _rb.velocity.x != 0f; // альтернативная запись верхней строчки
        _animator.SetFloat("yVelocity", _rb.velocity.y); // передаем в анимацию позицию игрока по y, чтобы запустить нужную анимацию, падения или прыжка
        _animator.SetBool("isMoving", _isMoving); // _animator обращается к Unity аниматору в котором есть переменная isMoving в которую присваиваем значение _isMoving, потому у нас включается анимация
        _animator.SetBool("isGrounded", _isGrounded); // _animator обращается к Unity аниматору в котором есть переменная isGrounded в которую присваиваем значение _isGrounded, потому у нас включается анимация
        _animator.SetBool("isDashing", dashTime > 0); // рапускаем рывок, если dashTime > 0
        _animator.SetBool("isAttacking", isAttacking); // анимация атаки
        _animator.SetInteger("comboCounter", comboCounter); // передает в анимацию счётчик комбо
    }

    private void FlipController()
    {
        if (_rb.velocity.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_rb.velocity.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

}

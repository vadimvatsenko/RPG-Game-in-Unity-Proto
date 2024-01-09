using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    private Rigidbody2D _rb; // можно в редакторе перетянуть Rigidbody2D или в методе Start получить его
    private Animator _animator; // объявляем переменную _animator (контроллер анимации)
    [SerializeField] private float moveSpeed = 5; // ускорение нашего игрока, так как xInput может быть -1 или 1, то это значение очень маленькое, потому нужно его умножать на ускорение
    [SerializeField] private float jumpForce = 5;
    private float _xInput; // создали публичную переменную, в нее будет записыватся положение Horizontal
    // private bool _isMoving; // переменная которая будет хранить в себе, двигается ли наш персонаж // удаляем эту переменную, так как она нам нужна будет только в методе AnimatorController()
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // теперь мы получили Rigidbody2D компонента, а сам Rigidbody2D остался приватным
        _animator = GetComponentInChildren<Animator>(); // получаем доступ к Animator в его ребёнке, В Player есть объект Animator. в Animator есть 
    }
    void Update()
    {
        Movement();
        CheckInput();
        AnimatorController();
    }

    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal"); // записываем в переменную xInput нажатие клавиш A и D
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void Movement()
    {
        _rb.velocity = new Vector2(_xInput * moveSpeed, _rb.velocity.y); // xInput(1 или -1) * на ускорение, по y равно самому себе
    }
    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        // реализован прыжок, по х собственное положение, по y - jumpForce
    }
    private void AnimatorController()
    {

        bool _isMoving = Math.Abs(_rb.velocity.x) > 0f; // _rb.velocity.x - может быть как положительным, так и отрицательным, потому мы к нему применяем Math.Abs - он конвертирует любое число в положительное
        //bool _isMoving = _rb.velocity.x != 0f; // альтернативная запись верхней строчки
        _animator.SetBool("isMoving", _isMoving); // _animator обращается к Unity аниматору в котором есть переменная isMoving в которую присваиваем значение _isMoving, потому у нас включается анимация

    }
}
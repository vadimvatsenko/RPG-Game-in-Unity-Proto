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
    private int _facingDir = 1; // по умолчанию маш персонаж смотрит в право, потому значение 1, если бы в противоположную смотрел, то было бы -1
    private bool _isFacingRight = true; // по умолчанию персонаж смотрит в право
    [Header("Collision info")] // группировка в самом редакторе на отдельные составляющие !!! Важно, после Header не должно идти приватных переменных
    [SerializeField] private float groundCheckDistance; // переменная, которая будет хранить в себе расстояние до земли
    [SerializeField] private LayerMask whatIsGround; // переменная в которой будет хранится слой который мы создали ground в начале этого раздела
    private bool _isGrounded; // переменная которая будет хранить в себе - находимся ли мы на земле или нет
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); // теперь мы получили Rigidbody2D компонента, а сам Rigidbody2D остался приватным
        _animator = GetComponentInChildren<Animator>(); // получаем доступ к Animator в его ребёнке, В Player есть объект Animator. в Animator есть 
    }
    void Update()
    {
        Movement();
        CheckInput();
        CollisionChecks();
        AnimatorController();
        FlipController();
    }
    private void CollisionChecks() // вынесли проверку столкновения с землёй в отдельный метод
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        // Physics2D.Raycast - выпускает луч от transform.position(позиции игрока) до Vector2.down (вниз) на расстояние groundCheckDistance, взаимодействует только с слоями, которые будут записаны в LayerMask whatIsGround.
        // возвращает true or false;
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
        if (_isGrounded) _rb.velocity = new Vector2(_rb.velocity.x, jumpForce); // реализован прыжок, по х собственное положение, по y - jumpForce // условие, если на земле выполни прыжок
    }
    private void AnimatorController()
    {
        bool _isMoving = Math.Abs(_rb.velocity.x) > 0f; // _rb.velocity.x - может быть как положительным, так и отрицательным, потому мы к нему применяем Math.Abs - он конвертирует любое число в положительное
        //bool _isMoving = _rb.velocity.x != 0f; // альтернативная запись верхней строчки
        _animator.SetFloat("yVelocity", _rb.velocity.y); // передаем в анимацию позицию игрока по y, чтобы запустить нужную анимацию, падения или прыжка
        _animator.SetBool("isMoving", _isMoving); // _animator обращается к Unity аниматору в котором есть переменная isMoving в которую присваиваем значение _isMoving, потому у нас включается анимация
        _animator.SetBool("isGrounded", _isGrounded); // _animator обращается к Unity аниматору в котором есть переменная isGrounded в которую присваиваем значение _isGrounded, потому у нас включается анимация
    }
    private void Flip()
    {
        _facingDir *= -1; // эта формула меняет переменную _facingDir на противоположную
        _isFacingRight = !_isFacingRight; // меняет переменную _isFacingRight на противоположную, то есть равен не себе
        transform.Rotate(0, 180, 0); // поворачивает игрока по оси y
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
    private void OnDrawGizmos() // метод в Unity который рисует вспомогательные линии на сцене
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance)); // Gizmos.DrawLine - нарисовать линию (от и до). От позиции игрока до позиции игрока по x и y минус groundCheckDistance
    }
}

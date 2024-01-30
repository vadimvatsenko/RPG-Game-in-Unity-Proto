using UnityEngine;
//5й скрипт
public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown; // время остывания даша
    private float dashUsageTimer; // 
    public float dashSpeed; // скорость даша
    public float dashDuration; // продолжительность даша
    public float dashDir { get; private set; } // переменная которая будет хранить в себе направление даша

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1; // направление игрока по умолчанию 1, { get; private set; } - означает, что мы можем получать данные из другого скрипта, но не изменять
    private bool facingRight = true;

    internal int xInput;


    // #region Components - группировка, можно скрыть контент
    #region Components 
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; } // это сделано для того, что бы мы могли вызвать anim из другого скрипта

    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(); // экземпляр класса PlayerStateMachine

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); // экземпляр класса PlayerIdleState в нём есть конструктор, который принимает игрока, состояние и анимацию

        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        airState = new PlayerAirState(this, stateMachine, "Jump");

        dashState = new PlayerDashState(this, stateMachine, "Dash");

        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");

        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        CheckForDashInput(); // вызываем метод запуск даша
    }

    private void CheckForDashInput() // метод который будет отвечать за запуск даша
    {
        if (WhatIsWallDecected())
        {
            return; // если мы коснулись земли выйди из этого метода
        }

        dashUsageTimer -= Time.deltaTime; // dashUsegeTimer будет постоянно уменьшатся

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) // если нажат shift И dashUsegeTimer меньше нуля
        {
            dashUsageTimer = dashCooldown; // после нажатия shift даш нельзя будет сделать, он будет остывать

            dashDir = Input.GetAxisRaw("Horizontal"); // получаем ось куда необходимо сделать даш, в зависимости от положении игрока

            if (dashDir == 0)
            {
                dashDir = facingDir; // если мы стоим на месте и подпрыгиваем, то даш в воздухе будет направлен туда, куда смотрит персонаж
            }
            stateMachine.ChangeState(dashState);
        }
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity); // передаем положение игрока по х
    }

    public bool whatIsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    // булевой метод, который возвращает, столкнулись ли мы з землёй. от groundCheck.position к низу, на дистанцию groundCheckDistance, маска whatIsGround
    public bool WhatIsWallDecected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    // булевой метод, который возвращает, столкнулись ли мы cо стеной. от wallCheck.position к право или лево(в зависимости, что facingDir(1 или -1)), на дистанцию wallCheckDistance, маска whatIsGround
    private void OnDrawGizmos() // рисуем линию к земле и стене
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        // от позиции земли(x,y,z) до позиции земли по x, а по y (позиция земли по y минус groundCheckDistance)
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

    }

    public void Flip() // метод переворота игрока
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    public void FlipController(float _x) // вызывает метод Flip()
    {
        if (_x > 0 && !facingRight) // если движение по x больше 0 и герой не повернут в лево. перевернись 
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }




}

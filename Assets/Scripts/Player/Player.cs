using UnityEngine;
//5й скрипт
public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;


    // #region Components - группировка, можно скрыить контент
    #region Components 
    public Rigidbody2D rb
    { get; private set; }
    public Animator anim { get; private set; } // это сделано для того, что бы мы могли вызвать anim из другого скрипта

    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine(); // экземпляр класса PlayerStateMachine

        idleState = new PlayerIdleState(this, stateMachine, "Idle"); // экземпляр класса PlayerIdleState в нём есть конструктор, который принимает игрока, состояние и анимацию

        moveState = new PlayerMoveState(this, stateMachine, "Move");

        jumpState = new PlayerJumpState(this, stateMachine, "Jump");

        airState = new PlayerAirState(this, stateMachine, "Jump");
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
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
    }

    public bool whatIsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    // будевы метод, который возвращает, столкнулись ли мы з землёй. от groundCheck.position к низу, на дистанцию groundCheckDistance, маска whatIsGround

    private void OnDrawGizmos() // рисуем линию к земле и стене
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        // от позиции земли(x,y,z) до позиции земли по x, а по y (позиция земли по y минус groundCheckDistance)
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

    }


}

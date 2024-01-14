using UnityEngine;

public class Enemy_Skeleton : Entity
{
    private bool _isAttacking;

    [Header("Move info")]
    [SerializeField] private float moveSpeed;

    [Header("Player detection")]
    [SerializeField] private float playerCheckDistance; // дистанция до игрока
    [SerializeField] private LayerMask whatIsPlayer; // маска игрока

    private RaycastHit2D _isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (_isPlayerDetected)
        {

            if (_isPlayerDetected.distance > 1) // если позиция до игрока больше 1 еденицы
            {
                _rb.velocity = new Vector2(moveSpeed * 1.5f * _facingDir, _rb.velocity.y);
                Debug.Log("I see the player");
                _isAttacking = false;
            }
            else
            {
                Debug.Log("Attack " + _isPlayerDetected.collider.gameObject.name);
                _isAttacking = true;
            }
        }

        if (!_isGrounded || _isWallDetected)
        { // если земли не окажется под ногами или столнётся со стеной переверни скелета в другую сторону
            Flip();
            Movement();
        }
    }

    private void Movement()
    {
        if (!_isAttacking)
        {
            _rb.velocity = new Vector2(moveSpeed * _facingDir, _rb.velocity.y);
        }

    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        _isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * _facingDir, whatIsPlayer);
        // лучь от позиции врага на право, дистанция до игрока уможиная на (-1 или 1 в зависимости от переменной _facingDir, слой игрока )
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * _facingDir, transform.position.y));
    }
}

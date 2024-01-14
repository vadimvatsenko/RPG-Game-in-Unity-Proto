using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("Move info")]
    [SerializeField] private float moveSpeed;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        _rb.velocity = new Vector2(moveSpeed * _facingDir, _rb.velocity.y);

        if (!_isGrounded || _isWallDetected)
        { // если земли не окажется под ногами или столнётся со стеной переверни скелета в другую сторону
            Flip();
        }
    }
}

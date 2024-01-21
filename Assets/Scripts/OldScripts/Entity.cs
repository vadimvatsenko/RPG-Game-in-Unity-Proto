using UnityEngine;

public class Entity : MonoBehaviour // это новый скрипт
{
    protected Animator _animator; // объявляем переменную _animator (контроллер анимации)
    protected Rigidbody2D _rb; // можно в редакторе перетянуть Rigidbody2D или в методе Start получить его
    protected int _facingDir = 1; // по умолчанию маш персонаж смотрит в право, потому значение 1, если бы в противоположную смотрел, то было бы -1
    protected bool _isFacingRight = true; // по умолчанию персонаж смотрит в право

    protected bool _isGrounded; // переменная которая будет хранить в себе - находимся ли мы на земле или нет
    protected bool _isWallDetected; // будет хранить, столнулись ли мы со стеной

    [Header("Collision info")] // группировка в самом редакторе на отдельные составляющие !!! Важно, после Header не должно идти приватных переменных
    [SerializeField] protected Transform groundCheck; // добавлена переменная которая хранит кординаты земли
    [SerializeField] protected float groundCheckDistance; // переменная, которая будет хранить в себе расстояние до земли
    [SerializeField] protected LayerMask whatIsGround; // переменная в которой будет хранится слой который мы создали ground в начале этого раздела

    [Space] // это для Header, чтобы отделить переменные в Unity
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;


    protected virtual void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();

        // для чего эта проверка, так как в Player нет объекта который сталкивается со стенами, Unity нам показывает ошибку, это из-за наследования
        if (wallCheck == null)
        {
            wallCheck = transform; // wallCheck равно координатам, которые есть у его родителя, это такой трюк
        }
    }


    protected virtual void Update()
    {
        CollisionChecks();
    }

    protected virtual void CollisionChecks() // вынесли проверку столкновения с землёй в отдельный метод
    {
        _isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        // Physics2D.Raycast - выпускает луч от transform.position(позиции игрока) до Vector2.down (вниз) на расстояние groundCheckDistance, взаимодействует только с слоями, которые будут записаны в LayerMask whatIsGround.
        // возвращает true or false;

        _isWallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * _facingDir, whatIsGround);
        // от позиции линии до права, на растояние wallCheckDistance * _facingDir (будет - или +, зависит от _facingDir) взаимодействует только с слоями, которые будут записаны в LayerMask whatIsGround.
    }

    protected virtual void Flip()
    {
        _facingDir *= -1; // эта формула меняет переменную _facingDir на противоположную
        _isFacingRight = !_isFacingRight; // меняет переменную _isFacingRight на противоположную, то есть равен не себе
        transform.Rotate(0, 180, 0); // поворачивает игрока по оси y
    }

    protected virtual void OnDrawGizmos() // метод в Unity который рисует вспомогательные линии на сцене
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance)); // Gizmos.DrawLine - нарисовать линию (от и до). От позиции игрока до позиции игрока по x и y минус groundCheckDistance
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * _facingDir, wallCheck.position.y));
        // рисует линию до стены, wallCheck.position.x + wallCheckDistance * _facingDir - будет менять направление линии в другую сторону - линия по x + длинна линии, и координаты по y - свои
    }
}


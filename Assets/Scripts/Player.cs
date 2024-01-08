using UnityEngine;
public class Player : MonoBehaviour
{
    private Rigidbody2D rb; // можно в редакторе перетянуть Rigidbody2D или в методе Start получить его
    [SerializeField] private float moveSpeed = 5; // ускорение нашего игрока, так как xInput может быть -1 или 1, то это значение очень маленькое, потому нужно его умножать на ускорение
    [SerializeField] private float jumpForce = 5;
    private float xInput; // создали публичную переменную, в нее будет записыватся положение Horizontal
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // теперь мы получили Rigidbody2D компонента, а сам Rigidbody2D остался приватным
    }
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal"); // записываем в переменную xInput нажатие клавиш A и D

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y); // xInput(1 или -1) * на ускорение, по y равно самому себе

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // реализован прыжок, по х собственное положение, по y - jumpForce
        }
    }
}
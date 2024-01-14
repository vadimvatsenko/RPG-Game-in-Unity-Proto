using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] protected string protectedSerializedField; // будет видна в обеих скриптах в редаторе Unity
    private string _private = "Приватая переменна, будет доступна только здесь";

    protected string _protected = "Не будет видна в других скриптах, но будет доступна для чтения в других скриптах";
    public string shapeName;
    public Rigidbody2D rb;
    public Vector2 velocity; // коодринаты x y
    // добавили две пучличных переменных, они будут доступны в обеих объектах, так как работает наследование
    public virtual void Start() // virtual - означает, что метод может быть унаследован от дочернего элемента скрипта 
    {
        Debug.Log("Hello, my shape is " + shapeName);
        rb.velocity = velocity; // расположение объектов = кординатам
    }

    protected virtual void ProtectedMethod() // если мы хотим защитить наш метод, то пишем protected, будет доступен только для чтения
    {
        Debug.Log(_protected);
    }
}

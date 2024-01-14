using UnityEngine;

public class Circle : Shape // будет унаследован от Shape
{
    public override void Start() // унаследование - override
    {
        base.Start(); // base.Start() - унаследование от базового класса Shape

        Debug.Log("I`m Inherated from Shape"); // можно дописать, что то своё, таким образом будет разный метод у квадрата и круга. Это полиморфизм

        ProtectedMethod();
    }
}

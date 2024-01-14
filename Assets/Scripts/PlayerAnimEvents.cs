using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    private Player _player; // переменная, которая будет хранить в себе скрипт который находится в родителе
    void Start()
    {
        _player = GetComponentInParent<Player>(); // получаем скрипт в родителе
    }

    private void AnimationTrigger()
    {
        _player.AttackOver(); // вызовет в скрипте метод AttackOver, который завершить анимацию атаки
    }
}

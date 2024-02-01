using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//13 этот скрипт будет висеть на объекте Animation
public class PlayerAnimationTrigger : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>(); // получаем сразу доступ к скрипту Player в родительском объекте Player

    private void AnimationTrigger() // метод который будет запускать метод AnimationTrigger() из скрипта Player
    {
        player.AnimationTrigger();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsMove : MonoBehaviour
{
    public Player player;    

    private void FixedUpdate()
    {       
        float aimAngle = Mathf.Atan2(player.moveDirection.x, player.moveDirection.z) * Mathf.Rad2Deg;               // находим угол игрока в градусах (player.moveDirection - это вектор движения игрока)
        Quaternion qua1 = Quaternion.Euler(transform.eulerAngles.x, aimAngle, transform.eulerAngles.z);             // создаем этот угол  
        transform.rotation = Quaternion.Lerp(transform.rotation, qua1, Time.fixedDeltaTime * 5f);                   // делаем Lerp   
    }
}

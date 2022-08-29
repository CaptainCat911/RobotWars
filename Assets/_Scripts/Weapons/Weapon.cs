using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponClass weaponClass;     // ссылка на класс оружия
    public Transform firePoint;         // якорь для снарядов
    public GameObject weaponHolder;
    Vector3 mousePosition;              // положение мыши
/*
    public float angleX;
    public float angleY;
    public float angleZ;*/

        // Параметры оружия (из класса оружия)
    GameObject bulletPrefab;            // префаб снаряда
    string weaponName;                  // название оружия
    float bulletSpeed;                  // скорость снаряда
    int damage;                         // урон (возможно нужно сделать на снаряде)
    float cooldown;                     // перезарядка оружия


    private void Start()
    {
        bulletPrefab = weaponClass.bullet;
        weaponName = weaponClass.name;
        bulletSpeed = weaponClass.bulletSpeed;
        damage = weaponClass.damage;
        cooldown = weaponClass.cooldown;
        GetComponent<Renderer>().material.color = weaponClass.color;
    }

    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);                                                                // положение мыши
        Vector3 aimDirection = mousePosition - firePoint.position;                                                                          // угол между положением мыши и якорем оружия
        float aimAngle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;                                                       // находим угол в градусах         
        Quaternion qua1 = Quaternion.Euler(weaponHolder.transform.eulerAngles.x, aimAngle, weaponHolder.transform.eulerAngles.z);           // создаем этот угол в Quaternion         
        weaponHolder.transform.rotation = Quaternion.Lerp(weaponHolder.transform.rotation, qua1, Time.fixedDeltaTime * 15f);                // делаем Lerp  
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);                                              // создаем префаб снаряда с позицией и поворотом якоря
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);                                                                             // поворачиваем снаряд (для ракеты)
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);                                      // даём импульс
    }
}

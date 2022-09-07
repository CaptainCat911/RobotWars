using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // Ссылки
    public Enemy enemy;
    public WeaponClass weaponClass;         // ссылка на класс оружия
    public Transform firePoint;             // якорь для снарядов
    GameObject weaponHolder;                // ссылка на weaponHolder (для поворота)   

    // Параметры оружия (из класса оружия)
    string weaponName;                      // название оружия
    GameObject bulletPrefab;                // префаб снаряда
    float bulletSpeed;                      // скорость снаряда
    int damage;                             // урон (возможно нужно сделать на снаряде)
    [HideInInspector]
    public float fireRate;                  // скорострельность оружия (10 - 0,1 выстрелов в секунду)
    [HideInInspector]
    public float nextTimeToFire = 0f;       // для стрельбы (когда стрелять в след раз)


    private void Awake()
    {
                
    }

    private void Start()
    {
        weaponName = weaponClass.name;                                          // имя
        bulletPrefab = weaponClass.bullet;                                      // тип снаряда
        bulletSpeed = weaponClass.bulletSpeed;                                  // скорость
        damage = weaponClass.damage;                                            // урон
        fireRate = weaponClass.fireRate;                                        // скорострельность
        GetComponent<Renderer>().material.color = weaponClass.color;            // цвет
        weaponHolder = GetComponentInParent<WeaponHolderEnemy>().gameObject;    // находим weaponHolder
        enemy = GetComponentInParent<Enemy>();
    }

    private void FixedUpdate()
    {   
        if (enemy.targetVisible)                    // направляем оружие на цель, если цель в зоне видимости
        {
            Vector3 aimDirection = enemy.target.transform.position - firePoint.position;                                                    // угол между позицией цели и якорем оружия
            float aimAngle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;                                                   // находим угол в градусах         
            Quaternion qua1 = Quaternion.Euler(weaponHolder.transform.eulerAngles.x, aimAngle, weaponHolder.transform.eulerAngles.z);       // создаем этот угол в Quaternion (ориентируемся на weaponHolder)        
            weaponHolder.transform.rotation = Quaternion.Lerp(weaponHolder.transform.rotation, qua1, Time.fixedDeltaTime * 20f);            // делаем Lerp между weaponHoder и нашим углом
        }
    }

    public void Fire()
    {
        //Debug.Log("Fire!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);                      // создаем префаб снаряда с позицией и поворотом якоря
        bullet.layer = 9;                                                                                           // назначаем снаряду слой "BulletEnemy"
        bullet.GetComponent<Bullet>().damage = damage;                                                              // присваиваем урон снаряду
        bullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);                                                     // поворачиваем снаряд (для ракеты)
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);              // даём импульс
    }
}

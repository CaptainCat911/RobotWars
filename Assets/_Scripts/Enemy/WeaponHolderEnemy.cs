using UnityEngine;

public class WeaponHolderEnemy : MonoBehaviour
{
    public Enemy enemy;
    public GameObject[] weapons;            // массив оружий    
    public bool rightHolder;                // правый или левый холдер
    EnemyWeapon currentWeapon;                   // текущее оружие


    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void Start()
    {
        int random = Random.Range(0, 2);      
        BuyWeapon(random);
    }

    private void Update()
    {
        // Стрельба
        if (enemy.readyToFire && currentWeapon && Time.time >= currentWeapon.nextTimeToFire && !rightHolder)  // для левого холдера
        {
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // вызываем функцию стрельбы у текущего оружия
        }
        if (enemy.readyToFire && currentWeapon && Time.time >= currentWeapon.nextTimeToFire && rightHolder)     // для правого холдера
        {
            //Debug.Log("Ready to fire");
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // вызываем функцию стрельбы у текущего оружия
        }       
    }

    void BuyWeapon(int weaponNumber)
    {
        GameObject weaponGO = Instantiate(weapons[weaponNumber], transform.position, transform.rotation);
        weaponGO.transform.SetParent(this.transform, true);
        currentWeapon = weaponGO.gameObject.GetComponentInChildren<EnemyWeapon>();          // получаем его скрипт
        weaponGO.SetActive(true);
    }
}
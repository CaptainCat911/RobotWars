using UnityEngine;

public class WeaponHolderEnemy : MonoBehaviour
{
    public Enemy enemy;
    public GameObject[] weapons;            // ������ ������    
    public bool rightHolder;                // ������ ��� ����� ������
    WeaponEnemy currentWeapon;                   // ������� ������


    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void Start()
    {
        BuyWeapon(0);
    }

    private void Update()
    {
        // ��������
        if (enemy.readyToFire && currentWeapon && Time.time >= currentWeapon.nextTimeToFire && !rightHolder)  // ��� ������ �������
        {
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // �������� ������� �������� � �������� ������
        }
        if (Time.time >= currentWeapon.nextTimeToFire && rightHolder)      // ��� ������� �������
        {
            Debug.Log("Ready to fire");
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // �������� ������� �������� � �������� ������
        }       
    }

    void BuyWeapon(int weaponNumber)
    {
        GameObject weaponGO = Instantiate(weapons[weaponNumber], transform.position, transform.rotation);
        weaponGO.transform.SetParent(this.transform, true);
        currentWeapon = weaponGO.gameObject.GetComponentInChildren<WeaponEnemy>();           // �������� ��� ������
        weaponGO.SetActive(true);
    }
}

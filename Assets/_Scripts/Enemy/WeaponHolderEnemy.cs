using UnityEngine;

public class WeaponHolderEnemy : MonoBehaviour
{
    public Enemy enemy;
    public GameObject[] weapons;            // ������ ������    
    public bool rightHolder;                // ������ ��� ����� ������
    EnemyWeapon currentWeapon;                   // ������� ������


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
        // ��������
        if (enemy.readyToFire && currentWeapon && Time.time >= currentWeapon.nextTimeToFire && !rightHolder)  // ��� ������ �������
        {
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // �������� ������� �������� � �������� ������
        }
        if (enemy.readyToFire && currentWeapon && Time.time >= currentWeapon.nextTimeToFire && rightHolder)     // ��� ������� �������
        {
            //Debug.Log("Ready to fire");
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // �������� ������� �������� � �������� ������
        }       
    }

    void BuyWeapon(int weaponNumber)
    {
        GameObject weaponGO = Instantiate(weapons[weaponNumber], transform.position, transform.rotation);
        weaponGO.transform.SetParent(this.transform, true);
        currentWeapon = weaponGO.gameObject.GetComponentInChildren<EnemyWeapon>();          // �������� ��� ������
        weaponGO.SetActive(true);
    }
}
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    // ������
    public Enemy enemy;
    public WeaponClass weaponClass;         // ������ �� ����� ������
    public Transform firePoint;             // ����� ��� ��������
    GameObject weaponHolder;                // ������ �� weaponHolder (��� ��������)   

    // ��������� ������ (�� ������ ������)
    string weaponName;                      // �������� ������
    GameObject bulletPrefab;                // ������ �������
    float bulletSpeed;                      // �������� �������
    int damage;                             // ���� (�������� ����� ������� �� �������)
    [HideInInspector]
    public float fireRate;                  // ���������������� ������ (10 - 0,1 ��������� � �������)
    [HideInInspector]
    public float nextTimeToFire = 0f;       // ��� �������� (����� �������� � ���� ���)


    private void Awake()
    {
                
    }

    private void Start()
    {
        weaponName = weaponClass.name;                                          // ���
        bulletPrefab = weaponClass.bullet;                                      // ��� �������
        bulletSpeed = weaponClass.bulletSpeed;                                  // ��������
        damage = weaponClass.damage;                                            // ����
        fireRate = weaponClass.fireRate;                                        // ����������������
        GetComponent<Renderer>().material.color = weaponClass.color;            // ����
        weaponHolder = GetComponentInParent<WeaponHolderEnemy>().gameObject;    // ������� weaponHolder
        enemy = GetComponentInParent<Enemy>();
    }

    private void FixedUpdate()
    {   
        if (enemy.targetVisible)                    // ���������� ������ �� ����, ���� ���� � ���� ���������
        {
            Vector3 aimDirection = enemy.target.transform.position - firePoint.position;                                                    // ���� ����� �������� ���� � ������ ������
            float aimAngle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;                                                   // ������� ���� � ��������         
            Quaternion qua1 = Quaternion.Euler(weaponHolder.transform.eulerAngles.x, aimAngle, weaponHolder.transform.eulerAngles.z);       // ������� ���� ���� � Quaternion (������������� �� weaponHolder)        
            weaponHolder.transform.rotation = Quaternion.Lerp(weaponHolder.transform.rotation, qua1, Time.fixedDeltaTime * 20f);            // ������ Lerp ����� weaponHoder � ����� �����
        }
    }

    public void Fire()
    {
        //Debug.Log("Fire!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);                      // ������� ������ ������� � �������� � ��������� �����
        bullet.layer = 9;                                                                                           // ��������� ������� ���� "BulletEnemy"
        bullet.GetComponent<Bullet>().damage = damage;                                                              // ����������� ���� �������
        bullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);                                                     // ������������ ������ (��� ������)
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);              // ��� �������
    }
}

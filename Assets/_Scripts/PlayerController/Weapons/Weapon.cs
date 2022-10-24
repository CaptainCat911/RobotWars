using UnityEngine;

public class Weapon : MonoBehaviour
{   
    // ������
    public WeaponClass weaponClass;         // ������ �� ����� ������
    public Transform firePoint;             // ����� ��� ��������
    GameObject weaponHolder;                // ������ �� weaponHolder (��� ��������)

    Vector3 mousePosition;                  // ��������� ����

    // ��������� ������ (�� ������ ������)
    string weaponName;                      // �������� ������
    GameObject bulletPrefab;                // ������ �������
    float bulletSpeed;                      // �������� �������
    int damage;                             // ���� (�������� ����� ������� �� �������)
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
        //GetComponent<Renderer>().material.color = weaponClass.color;            // ����
        weaponHolder = GetComponentInParent<WeaponHolder>().gameObject;         // ������� weaponHolder
    }

    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);                                                            // ��������� ����
        Vector3 aimDirection = mousePosition - firePoint.position;                                                                      // ���� ����� ���������� ���� � ������ ������
        float aimAngle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;                                                   // ������� ���� � ��������         
        Quaternion qua1 = Quaternion.Euler(weaponHolder.transform.eulerAngles.x, aimAngle, weaponHolder.transform.eulerAngles.z);       // ������� ���� ���� � Quaternion (������������� �� weaponHolder)        
        weaponHolder.transform.rotation = Quaternion.Lerp(weaponHolder.transform.rotation, qua1, Time.fixedDeltaTime * 15);             // ������ Lerp ����� weaponHoder � ����� �����        
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);                      // ������� ������ ������� � �������� � ��������� �����
        bullet.layer = 7;                                                                                           // ��������� ������� ���� "BulletPlayer"
        bullet.GetComponent<Bullet>().damage = damage;                                                              // ����������� ���� �������
        bullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);                                                     // ������������ ������ (��� ������)
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);              // ��� �������
    }
}

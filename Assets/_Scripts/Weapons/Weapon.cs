using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponClass weaponClass;     // ������ �� ����� ������
    public Transform firePoint;         // ����� ��� ��������
    public GameObject weaponHolder;
    Vector3 mousePosition;              // ��������� ����
/*
    public float angleX;
    public float angleY;
    public float angleZ;*/

        // ��������� ������ (�� ������ ������)
    GameObject bulletPrefab;            // ������ �������
    string weaponName;                  // �������� ������
    float bulletSpeed;                  // �������� �������
    int damage;                         // ���� (�������� ����� ������� �� �������)
    float cooldown;                     // ����������� ������


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
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);                                                                // ��������� ����
        Vector3 aimDirection = mousePosition - firePoint.position;                                                                          // ���� ����� ���������� ���� � ������ ������
        float aimAngle = Mathf.Atan2(aimDirection.x, aimDirection.z) * Mathf.Rad2Deg;                                                       // ������� ���� � ��������         
        Quaternion qua1 = Quaternion.Euler(weaponHolder.transform.eulerAngles.x, aimAngle, weaponHolder.transform.eulerAngles.z);           // ������� ���� ���� � Quaternion         
        weaponHolder.transform.rotation = Quaternion.Lerp(weaponHolder.transform.rotation, qua1, Time.fixedDeltaTime * 15f);                // ������ Lerp  
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);                                              // ������� ������ ������� � �������� � ��������� �����
        bullet.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);                                                                             // ������������ ������ (��� ������)
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);                                      // ��� �������
    }
}

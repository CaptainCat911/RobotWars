using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public GameObject[] weapons;            // ������ ������    
    public bool rightHolder;                // ������ ��� ����� ������
    Weapon currentWeapon;                   // ������� ������
    int selectedWeapon = 0;                 // ������ ������ (��������� � �������� WeaponHolder)

    void Start()
    {      
        BuyWeapon(0);
        BuyWeapon(1);
        //BuyWeapon(2);
        if (rightHolder)
        {
            selectedWeapon = 1;
        }
        SelectWeapon();
    }

    private void Update()
    {
        // ��������
        if (Input.GetMouseButton(0) && currentWeapon && Time.time >= currentWeapon.nextTimeToFire && !rightHolder)  // ��� ������ �������
        {
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // �������� ������� �������� � �������� ������
        }
        if (Input.GetMouseButton(1) && currentWeapon && Time.time >= currentWeapon.nextTimeToFire && rightHolder)   // ��� ������� �������
        {
            currentWeapon.nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
            currentWeapon.Fire();                                                           // �������� ������� �������� � �������� ������
        }

        // ����� ������
        int previousWeapon = selectedWeapon;                                                // ����������� ���������� ������ ������

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && rightHolder)                         // ���������� �������� (��� ������� �������)
        {
            if (selectedWeapon >= transform.childCount - 1)                                 // ���������� � 0 ������, ���� ������ ����� ���-�� ������� � �������� WeaponHolder - 1(?)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && !rightHolder)                        // ���������� �������� (��� ������ �������)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

/*        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }*/

        if (previousWeapon != selectedWeapon)                   // ���� ������ ������ ��������� - �������� �������
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);                                      // ���������� ������ � ��������
                currentWeapon = weapon.gameObject.GetComponentInChildren<Weapon>();     // �������� ��� ������
            }
            else
                weapon.gameObject.SetActive(false);                                     // ��������� ������ �������������
            i++;
        }
    }

    void BuyWeapon(int weaponNumber)
    {
        GameObject weaponGO = Instantiate(weapons[weaponNumber], transform.position, transform.rotation);
        weaponGO.transform.SetParent(this.transform, true);
        weaponGO.SetActive(false);
    }
}

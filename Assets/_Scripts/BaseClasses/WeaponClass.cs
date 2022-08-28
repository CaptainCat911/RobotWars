using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponClass : ScriptableObject
{
    public string weaponName;
    public Color color;
    public int damage;
    public int bulletSpeed;
    public float cooldown;
    public GameObject bullet;
}

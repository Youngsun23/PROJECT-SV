using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item/Weapon")]
public class WeaponItem : Item
{
    public int Damage => damage;
    public float Radius => radius;

    [SerializeField] private int damage;
    [SerializeField] private float radius;
}

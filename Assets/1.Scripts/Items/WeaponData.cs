using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponStat
{
    public string weaponName;
    public float LifeTime;
    public float moveSpeed;
    public float Damage;
    public float shootingDelay;
    public int shootingCount;
    public float meleeDamage;
    public Sprite myImg;

    public WeaponStat(string wn, float lt, float ms, float dmg, float shd, int shc, float md, Sprite i)
    {
        weaponName = wn;
        LifeTime = lt;
        moveSpeed = ms;
        Damage = dmg;
        shootingDelay = shd;
        shootingCount = shc;
        meleeDamage = md;
        myImg = i;
    }
}

[CreateAssetMenu(fileName = "Weapon Data", menuName = "ScriptableObject/Weapon Data", order = -1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] WeaponStat[] weaponStat;

    public WeaponStat[] weaponStats
    {
        get => weaponStat;
    }

    public WeaponStat getWeaponStat(string weaponName)
    {
        return EquipmentManager.Instance.GetWeaponStat(weaponName);
        //for(int i = 0; i < weaponStat.Length; i++)
        //{
        //    if (weaponStat[i].weaponName.Equals(weaponName))
        //    {
        //        return weaponStat[i];
        //    }
        //}

        //return null;
    }
}

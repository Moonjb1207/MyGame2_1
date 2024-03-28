using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Items
{
    public WeaponStat stat;
    public bool IsAttacking;
    public Transform shootTr;



    private void Start()
    {
        stat = EquipmentManager.Instance.weaponData.getWeaponStat(stat.weaponName);
        myType = ItemType.weapon;
    }

    public virtual void UpgradeWeapon(float val1, float val2 = 0, float val3 = 0)
    {
        stat.Damage += val1;
    }

    public abstract void Attack(float atkpoint);
}
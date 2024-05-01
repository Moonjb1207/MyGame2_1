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

        stat.shootingDelay = Mathf.Clamp(stat.shootingDelay - stat.shootingDelay * 0.1f,
            EquipmentManager.Instance.weaponData.getWeaponStat(stat.weaponName).shootingDelay * 0.5f, 
            EquipmentManager.Instance.weaponData.getWeaponStat(stat.weaponName).shootingDelay);

        stat.shootingCount = Mathf.Clamp(stat.shootingCount + (int)InventoryManager.Instance.myWeaponDic[stat.weaponName] / 2,
            EquipmentManager.Instance.weaponData.getWeaponStat(stat.weaponName).shootingCount,
            EquipmentManager.Instance.weaponData.getWeaponStat(stat.weaponName).shootingCount * 5);

        //stat.shootingDelay = stat.shootingDelay * val2;
        //stat.shootingCount = InventoryManager.Instance.myWeaponDic[stat.weaponName] / 10;
    }
    
    public abstract void Attack(float atkpoint);
}
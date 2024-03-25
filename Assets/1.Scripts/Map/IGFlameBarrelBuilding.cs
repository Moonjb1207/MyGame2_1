using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGFlameBarrelBuilding : IGBuilding
{
    public LayerMask myEnemy;
    public GameObject explosiveEffect;

    public override void SetStat(string name)
    {
        myName = name;
        curHP = EquipmentManager.Instance.GetBuildingStat(myName).buildingHP;
        Delay = EquipmentManager.Instance.GetBuildingStat(myName).delay + (InventoryManager.Instance.mybuildingDic[myName] - 1);
        Value = EquipmentManager.Instance.GetBuildingStat(myName).value + (InventoryManager.Instance.mybuildingDic[myName] - 1) / 10;
        KeepTime = EquipmentManager.Instance.GetBuildingStat(myName).keepTime;
        DamageTime = EquipmentManager.Instance.GetBuildingStat(myName).damageTime;
    }

    public override void OnDamage(float dmg)
    {
        if (curHP > 0)
            curHP -= dmg;

        if (curHP <= 0)
        {
            Collider[] list = Physics.OverlapSphere(transform.position, 4.0f, myEnemy);

            if (list != null)
            {
                foreach (Collider col in list)
                {
                    IBattle ib = col.GetComponent<IBattle>();
                    ib?.OnDamage(Delay);

                    Enemy enemy = col.GetComponent<Enemy>();
                    enemy?.AddDeBuff(new DeBuff(DeBuffType.Burn, KeepTime, Value, DamageTime));
                }
            }

            GameObject temp = Instantiate(explosiveEffect);
            temp.transform.position = transform.position;

            Destroy(transform.parent.gameObject);
        }
    }
}

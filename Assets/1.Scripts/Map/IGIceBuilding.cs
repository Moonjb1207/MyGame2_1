using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGIceBuilding : IGBuilding
{
    float curDelay = 0.0f;
    public LayerMask myEnemy;
    public GameObject iceEffect;

    public override void SetStat(string name)
    {
        myName = name;
        curHP = EquipmentManager.Instance.GetBuildingStat(myName).buildingHP;
        Delay = EquipmentManager.Instance.GetBuildingStat(myName).delay;
        Value = EquipmentManager.Instance.GetBuildingStat(myName).value + (InventoryManager.Instance.mybuildingDic[myName] - 1) / 10;
        KeepTime = EquipmentManager.Instance.GetBuildingStat(myName).keepTime;
        DamageTime = EquipmentManager.Instance.GetBuildingStat(myName).damageTime;
    }

    private void Start()
    {
        curDelay = Delay;
    }

    private void Update()
    {
        curDelay -= Time.deltaTime;

        if(curDelay < 0.0f)
        {
            Collider[] list = Physics.OverlapSphere(transform.position, 6.0f, myEnemy);

            GameObject temp = Instantiate(iceEffect);
            temp.transform.position = transform.position;

            if (list != null)
            {
                foreach(Collider col in list)
                {
                    Enemy enemy = col.GetComponent<Enemy>();
                    enemy.AddDeBuff(new DeBuff(DeBuffType.Slow, KeepTime, Value, DamageTime));
                }
            }

            curDelay = Delay;
        }
    }
}

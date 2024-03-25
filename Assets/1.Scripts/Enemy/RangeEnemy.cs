using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    public RangeWeapon weaponPrefab;
    public Transform shootTr;

    public override void OnAttack()
    {
        curEnemyState.myAnim.SetTrigger("Attacking");
    }

    public void Shooting()
    {
        RangeWeapon myWeapon = EnemyWeaponPool.Instance.DequeueWeapon();

        myWeapon.transform.position = shootTr.position;
        myWeapon.Shoot(curEnemyState.bodyTr.transform.forward, data.damage, 3, 4);
        myWeapon.gameObject.SetActive(true);
    }
}

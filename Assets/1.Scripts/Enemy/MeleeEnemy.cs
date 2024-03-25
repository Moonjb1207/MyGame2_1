using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public override void OnAttack()
    {
        IBattle ib = target.GetComponent<IBattle>();
        ib?.OnDamage(data.damage);
        curEnemyState.myAnim.SetTrigger("Attacking");
    }
}

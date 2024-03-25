using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    public GameObject myBombEffect;

    public LayerMask DamageEnemy;

    public override void OnAttack()
    {
        curEnemyState.myAnim.SetTrigger("Attacking");
        curEnemyState.myAnim.SetBool("IsAttacking", true);
    }

    public void BombAttack()
    {
        Collider[] list = Physics.OverlapSphere(transform.position, 5.0f, DamageEnemy);

        if (list != null)
        {
            foreach (Collider col in list)
            {
                col.GetComponent<IBattle>().OnDamage(data.damage);
            }
        }

        GameObject temp = Instantiate(myBombEffect);
        temp.transform.position = transform.position;

        gameObject.SetActive(false);
        mySpawn.MyEnemyDead();
        EnemyPool.Instance.EnqueueEnemy(this, myNum);
    }

    private void OnDrawGizmos()
    {
        if (transform.position == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4.0f);
    }
}

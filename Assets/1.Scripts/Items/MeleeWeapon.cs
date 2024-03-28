using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public Transform attackPos;
    public LayerMask myEnemy;
    public AudioClip shootSound;

    public override void Attack(float atkpoint)
    {
        SoundManager.Instance.PlayEfSound(attackPos.position, shootSound);

        Collider[] list = Physics.OverlapSphere(attackPos.position, stat.LifeTime, myEnemy);
        if (list != null)
        {
            foreach (Collider col in list)
            {
                IBattle ib = col.GetComponent<IBattle>();
                ib?.OnDamage(stat.meleeDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, stat.LifeTime);
    }
}

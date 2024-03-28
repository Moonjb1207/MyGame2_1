using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Weapon
{
    public Bullet bulletPrefab;
    public AudioClip shootSound;

    public override void Attack(float atkpoint)
    {
        StartCoroutine(shootingBullets(stat.shootingCount, stat.shootingDelay, atkpoint));
    }

    public virtual IEnumerator shootingBullets(int count, float delay, float atkpoint)
    {
        IsAttacking = true;
        while (count != 0)
        {
            count--;
            Shooting(atkpoint);
            yield return new WaitForSeconds(delay);
        }
        IsAttacking = false;
    }

    public virtual void Shooting(float atkpoint)
    {
        Bullet bullet = BulletPool.Instance.DequeueBullet();

        bullet.transform.position = shootTr.position;
        bullet.Shoot(transform.forward, stat.Damage + atkpoint, stat.LifeTime, stat.moveSpeed);
        bullet.gameObject.SetActive(true);

        SoundManager.Instance.PlayEfSound(shootTr.position, shootSound);
    }
}

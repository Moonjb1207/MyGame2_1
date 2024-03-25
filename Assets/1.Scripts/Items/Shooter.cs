using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Weapon
{
    public Bullet bulletPrefab;
    public AudioClip shootSound;

    public override void Attack()
    {
        StartCoroutine(shootingBullets(stat.shootingCount, stat.shootingDelay));
    }

    public virtual IEnumerator shootingBullets(int count, float delay)
    {
        IsAttacking = true;
        while (count != 0)
        {
            count--;
            Shooting();
            yield return new WaitForSeconds(delay);
        }
        IsAttacking = false;
    }

    public virtual void Shooting()
    {
        Bullet bullet = BulletPool.Instance.DequeueBullet();

        bullet.transform.position = shootTr.position;
        bullet.Shoot(transform.forward, stat.Damage, stat.LifeTime, stat.moveSpeed);
        bullet.gameObject.SetActive(true);

        SoundManager.Instance.PlayEfSound(shootTr.position, shootSound);
    }
}

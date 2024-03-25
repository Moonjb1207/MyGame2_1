using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Shooter
{
    public float angle;
    public int dirCount = 3;

    public override void Shooting()
    {
        float startAngle = (angle * dirCount / 2f) - angle / 2;

        Vector3 dir = transform.forward;

        for(int i = 0; i < dirCount; i++)
        {
            Bullet bullet = BulletPool.Instance.DequeueBullet();

            Vector3 shootingDir = Quaternion.AngleAxis(startAngle, Vector3.up) * dir;

            bullet.transform.position = shootTr.position;
            bullet.Shoot(shootingDir, stat.Damage, stat.LifeTime, stat.moveSpeed);
            bullet.gameObject.SetActive(true);

            startAngle -= angle;
        }

        SoundManager.Instance.PlayEfSound(shootTr.position, shootSound);

        //for(int i = -1; i < 2; i++)
        //{
        //    Bullet bullet = BulletPool.Instance.DequeueBullet();

        //    bullet.transform.position = shootTr.position;
        //    bullet.Shoot(transform.forward + new Vector3(i * 0.3f, 0, 0), stat.Damage, stat.LifeTime, stat.moveSpeed);
        //    bullet.gameObject.SetActive(true);
        //}
    }
}

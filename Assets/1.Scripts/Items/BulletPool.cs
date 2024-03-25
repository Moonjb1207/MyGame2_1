using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool instance;
    public static BulletPool Instance => instance;

    public Bullet[] bullets;
    public Queue<Bullet> bulletQueue = new Queue<Bullet>();
    public Bullet bulletPrefab;

    private void Awake()
    {
        instance = this;

        bullets = GetComponentsInChildren<Bullet>(true);

        for (int i = 0; i < bullets.Length; i++)
        {
            bulletQueue.Enqueue(bullets[i]);
        }
    }

    public Bullet DequeueBullet()
    {
        Bullet myBullet;

        if (bulletQueue.Count == 0)
        {
            myBullet = Instantiate(bulletPrefab);
        }
        else
        {
            myBullet = bulletQueue.Dequeue();
        }

        myBullet.transform.SetParent(null);
        return myBullet;
    }

    public void EnqueueBullet(Bullet bullet)
    {
        bullet.transform.SetParent(transform);

        bulletQueue.Enqueue(bullet);
    }
}

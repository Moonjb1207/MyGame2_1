using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask myBlock;
    public LayerMask myEnemy;
    public float LifeTime;
    public float moveSpeed;
    public float Damage;

    Vector3 direction = Vector3.forward;

    private void OnEnable()
    {
        
    }

    private void Update()
    {
        LifeTime -= Time.deltaTime;

        if(LifeTime < 0)
        {
            BulletPool.Instance.EnqueueBullet(this);
            gameObject.SetActive(false);
            return;
        }

        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = transform.forward;

        float delta = moveSpeed * Time.deltaTime;

        if (Physics.Raycast(ray, out RaycastHit hit, delta, myBlock))
        {
            if ((myBlock & 1 << hit.transform.gameObject.layer) != 0)
            {
                if ((myEnemy & 1 << hit.transform.gameObject.layer) != 0)
                {
                    IBattle ib = hit.transform.GetComponent<IBattle>();
                    ib?.OnDamage(Damage);
                }

                gameObject.SetActive(false);
                BulletPool.Instance.EnqueueBullet(this);
            }
        }
        else
        {
            transform.Translate(transform.forward.normalized * delta, Space.World);
        }
    }

    public void Shoot(Vector3 dir, float d, float lt, float ms)
    {
        direction = dir;
        Damage = d;
        LifeTime = lt;
        moveSpeed = ms;

        transform.forward = direction;
    }
}

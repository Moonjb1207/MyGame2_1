using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public LayerMask myEnemy;
    public LayerMask myBlock;
    public float LifeTime;
    public float moveSpeed;
    public float Damage;

    public float DamageTime;
    public float KeepTime;
    public float Value;

    public GameObject expEffect;

    private void OnEnable()
    {
        DamageTime = 1.0f;
        Value = 0.5f;
        KeepTime = 3.0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if((myBlock & 1 << other.gameObject.layer) != 0)
        {
            StopAllCoroutines();
            Collider[] list = Physics.OverlapSphere(transform.position + new Vector3(0, 0, 0.5f), 5.0f, myEnemy);
            if (list != null)
            {
                foreach (Collider col in list)
                {
                    col.GetComponent<IBattle>().OnDamage(Damage);
                    col.GetComponent<Enemy>().AddDeBuff(new DeBuff(DeBuffType.Burn, KeepTime, Value, DamageTime));
                }
            }

            GameObject temp = Instantiate(expEffect);
            temp.transform.position = transform.position;
            MissilePool.Instance.EnqueueMissile(this);
            gameObject.SetActive(false);
        }
    }

    IEnumerator movingMissile()
    {
        while (LifeTime >= 0)
        {
            LifeTime -= Time.deltaTime;

            float delta = moveSpeed * Time.deltaTime;
            transform.Translate(transform.forward.normalized * delta, Space.World);

            yield return null;
        }

        Collider[] list = Physics.OverlapSphere(transform.position + new Vector3(0, 0, 0.5f), 5.0f, myEnemy);
        if (list != null)
        {
            foreach (Collider col in list)
            {
                col.GetComponent<IBattle>().OnDamage(Damage);
            }
        }
        GameObject temp = Instantiate(expEffect);
        temp.transform.position = transform.position;
        MissilePool.Instance.EnqueueMissile(this);
        gameObject.SetActive(false);
    }

    public void Shooting()
    {
        StartCoroutine(movingMissile());
    }

    public void Create(Vector3 dir, float d, float lt, float ms)
    {
        transform.forward = dir;
        Damage = d;
        LifeTime = lt;
        moveSpeed = ms;
    }
}

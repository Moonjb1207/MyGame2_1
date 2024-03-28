using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public Missile missilePrefab;
    public Missile myMissile;

    public AudioClip shootSound;

    private void Awake()
    {
        //CreateMissile();
    }

    public override void Attack(float atkpoint)
    {
        if (myMissile == null) return;

        StartCoroutine(shootingMissile(stat.shootingCount, stat.shootingDelay, atkpoint));
        SoundManager.Instance.PlayEfSound(shootTr.position, shootSound);
    }

    IEnumerator shootingMissile(int count, float delay, float atkpoint)
    {
        IsAttacking = true;
        while (count != 0)
        {
            count--;
            myMissile.Shooting();
            myMissile.transform.SetParent(null);
            yield return new WaitForSeconds(delay);
        }
        IsAttacking = false;

        CreateMissile(atkpoint);
    }

    public void CreateMissile(float atkpoint)
    {
        myMissile = MissilePool.Instance.DequeueMissile(this);

        myMissile.transform.position = shootTr.position;
        myMissile.Create(transform.forward, stat.Damage + atkpoint, stat.LifeTime, stat.moveSpeed);
        myMissile.gameObject.SetActive(true);
    }
}

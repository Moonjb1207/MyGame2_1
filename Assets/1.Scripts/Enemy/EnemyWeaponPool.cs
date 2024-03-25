using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponPool : MonoBehaviour
{
    private static EnemyWeaponPool instance;
    public static EnemyWeaponPool Instance => instance;

    public RangeWeapon[] rangeWeapons;
    public Queue<RangeWeapon> rangeWeaponQueue = new Queue<RangeWeapon>();
    public RangeWeapon rangeWeaponPrefab;

    private void Awake()
    {
        instance = this;

        rangeWeapons = GetComponentsInChildren<RangeWeapon>(true);

        for (int i = 0; i < rangeWeapons.Length; i++)
        {
            rangeWeaponQueue.Enqueue(rangeWeapons[i]);
        }
    }

    public RangeWeapon DequeueWeapon()
    {
        RangeWeapon myRWeapon;

        if (rangeWeaponQueue.Count == 0)
        {
            myRWeapon = Instantiate(rangeWeaponPrefab);
        }
        else
        {
            myRWeapon = rangeWeaponQueue.Dequeue();
        }

        myRWeapon.transform.SetParent(null);
        return myRWeapon;
    }

    public void EnqueueWeapon(RangeWeapon rweap)
    {
        rweap.transform.SetParent(transform);

        rangeWeaponQueue.Enqueue(rweap);
    }
}

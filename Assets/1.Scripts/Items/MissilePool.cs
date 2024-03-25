using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    private static MissilePool instance;
    public static MissilePool Instance => instance;

    public Missile[] missiles;
    public Queue<Missile> missileQueue = new Queue<Missile>();
    public Missile missilePrefab;

    private void Awake()
    {
        instance = this;

        missiles = GetComponentsInChildren<Missile>(true);

        for (int i = 0; i < missiles.Length; i++)
        {
            missileQueue.Enqueue(missiles[i]);
        }
    }

    public Missile DequeueMissile(RocketLauncher myRocket)
    {
        Missile myMissile;

        if (missileQueue.Count == 0)
        {
            myMissile = Instantiate(missilePrefab);
        }
        else
        {
            myMissile = missileQueue.Dequeue();
        }

        myMissile.transform.SetParent(myRocket.transform);
        return myMissile;
    }

    public void EnqueueMissile(Missile missile)
    {
        missile.transform.SetParent(transform);

        missileQueue.Enqueue(missile);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool instance;
    public static EnemyPool Instance => instance;

    public List<Transform> Pools;

    public MeleeEnemy[] melees;
    public Queue<MeleeEnemy> meleeQueue = new Queue<MeleeEnemy>();
    public MeleeEnemy meleePrefab;

    public RangeEnemy[] ranges;
    public Queue<RangeEnemy> rangeQueue = new Queue<RangeEnemy>();
    public RangeEnemy rangePrefab;

    public BombEnemy[] bombs;
    public Queue<BombEnemy> bombQueue = new Queue<BombEnemy>();
    public BombEnemy bombPrefab;


    private void Awake()
    {
        instance = this;

        for (int i = 0; i < (int)EnemyQueueNum.End; i++)
        {
            Pools.Add(transform.GetChild(i));
        }

        melees = GetComponentsInChildren<MeleeEnemy>(true);

        for (int i = 0; i < melees.Length; i++)
        {
            meleeQueue.Enqueue(melees[i]);
        }

        ranges = GetComponentsInChildren<RangeEnemy>(true);

        for (int i = 0; i < ranges.Length; i++)
        {
            rangeQueue.Enqueue(ranges[i]);
        }

        bombs = GetComponentsInChildren<BombEnemy>(true);

        for (int i = 0; i < bombs.Length; i++)
        {
            bombQueue.Enqueue(bombs[i]);
        }
    }

    public Enemy DequeueEnemy(EnemyQueueNum num, EnemyRespawn myspawn)
    {
        Enemy myEnemy;

        if (num == EnemyQueueNum.Melee)
        {
            if (meleeQueue.Count == 0)
            {
                myEnemy = Instantiate(meleePrefab);
                myEnemy.SaveMySpawn(myspawn);
            }
            else
            {
                myEnemy = meleeQueue.Dequeue();
                myEnemy.SaveMySpawn(myspawn);
            }
        }
        else if (num == EnemyQueueNum.Range)
        {
            if (rangeQueue.Count == 0)
            {
                myEnemy = Instantiate(rangePrefab);
                myEnemy.SaveMySpawn(myspawn);
            }
            else
            {
                myEnemy = rangeQueue.Dequeue();
                myEnemy.SaveMySpawn(myspawn);
            }
        }
        else
        {
            if (bombQueue.Count == 0)
            {
                myEnemy = Instantiate(bombPrefab);
                myEnemy.SaveMySpawn(myspawn);
            }
            else
            {
                myEnemy = bombQueue.Dequeue();
                myEnemy.SaveMySpawn(myspawn);
            }
        }

        myEnemy.transform.SetParent(null);
        return myEnemy;
    }

    public void EnqueueEnemy(Enemy enemy, EnemyQueueNum num)
    {
        enemy.transform.SetParent(Pools[(int)num]);

        if(num == EnemyQueueNum.Melee)
        {
            MeleeEnemy myEnemy = enemy.GetComponent<MeleeEnemy>();
            meleeQueue.Enqueue(myEnemy);
        }
        else if(num == EnemyQueueNum.Range)
        {
            RangeEnemy myEnemy = enemy.GetComponent<RangeEnemy>();
            rangeQueue.Enqueue(myEnemy);
        }
        else
        {
            BombEnemy myEnemy = enemy.GetComponent<BombEnemy>();
            bombQueue.Enqueue(myEnemy);
        }
    }
}

public enum EnemyQueueNum
{
    Melee,
    Range,
    Bomb,
    End
}
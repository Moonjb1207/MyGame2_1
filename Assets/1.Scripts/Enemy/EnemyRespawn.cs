using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRespawn : MonoBehaviour
{
    public int respawnCount;
    public int maxCount;
    public int curCount;
    public float respawnDelay;
    public float curDelay;
    public bool isStart;

    NavMeshPath checkPath = null;

    // Start is called before the first frame update
    void Start()
    {
        checkPath = new NavMeshPath();
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        curDelay += Time.deltaTime;
        if(curDelay >= respawnDelay && curCount < maxCount && respawnCount != 0)
        {
            curDelay = 0;
            curCount++;
            respawnCount--;

            int rnd = Random.Range(0, 10);
            if(rnd < 2)
            {
                Enemy myEnemy = EnemyPool.Instance.DequeueEnemy(EnemyQueueNum.Bomb, this);
                myEnemy.transform.position = transform.position;
                myEnemy.gameObject.SetActive(true);
            }
            else if(rnd < 5)
            {
                Enemy myEnemy = EnemyPool.Instance.DequeueEnemy(EnemyQueueNum.Range, this);
                myEnemy.transform.position = transform.position;
                myEnemy.gameObject.SetActive(true);
            }
            else if (rnd < 10)
            {
                Enemy myEnemy = EnemyPool.Instance.DequeueEnemy(EnemyQueueNum.Melee, this);
                myEnemy.transform.position = transform.position;
                myEnemy.gameObject.SetActive(true);
            }
        }

        if (respawnCount == 0 && curCount == 0 && isStart)
        {
            EndSpawn();
            isStart = false;
        }
    }

    public void WaveStart(int rc, int mc, int rd)
    {
        //respawnCount = myStageStat.respawnCount;
        //maxCount = myStageStat.maxCount;
        //respawnDelay = myStageStat.respawnDelay;

        respawnCount = rc;
        maxCount = mc;
        respawnDelay = rd;

        curCount = 0;
        curDelay = 0;
        isStart = true;
    }

    public void MyEnemyDead()
    {
        curCount--;
    }

    public void EndSpawn()
    {
        GetComponentInParent<InGameManager>().spawnerCount--;
    }

    public bool CheckPath()
    {
        NavMesh.CalculatePath(transform.position, Player.Instance.transform.position, NavMesh.AllAreas, checkPath);

        if (checkPath.status == NavMeshPathStatus.PathPartial
            || checkPath.status == NavMeshPathStatus.PathInvalid)
            return false;

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public Enemy enemy;
    public Transform bodyTr;
    public Animator myAnim;
    public float curDelay;

    public virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
        bodyTr = transform.Find("Body");
        myAnim = bodyTr.GetComponent<Animator>();
        curDelay = 0;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
}

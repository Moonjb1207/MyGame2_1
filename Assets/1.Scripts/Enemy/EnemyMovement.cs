using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NextAction();

public class EnemyMovement : MonoBehaviour
{
    Coroutine coFollow = null;

    public Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FollowTarget(Transform target, float moveSpeed, float attackRange, NextAction reached = null)
    {
        if (coFollow != null) StopCoroutine(coFollow);
        coFollow = StartCoroutine(FollowingTarget(target, moveSpeed, attackRange));

        StartCoroutine(CoAttack(target, attackRange, reached));
    }


    IEnumerator FollowingTarget(Transform target, float moveSpeed, float attackRange)
    {
        while(target != null)
        {
            //float rdelta = rotSpeed * Time.deltaTime;

            Vector3 dir = target.position - transform.position;

            dir.y = 0.0f;

            //Vector3 rot =
            //    Vector3.RotateTowards(transform.forward, dir, rdelta * Mathf.Deg2Rad, 0.0f);
            //transform.rotation = Quaternion.LookRotation(rot);

            transform.forward = dir;

            if (dir.magnitude > attackRange && !myAnim.GetBool("IsAttacking"))
            {
                myAnim.SetBool("IsMoving", true);

                float delta = moveSpeed * Time.deltaTime;

                if(delta > dir.magnitude - attackRange)
                {
                    delta = dir.magnitude - attackRange;
                }

                transform.Translate(dir.normalized * delta, Space.World);
            }

            yield return null;
        }
    }

    IEnumerator CoAttack(Transform target, float attackRange, NextAction reached = null, float attackDelay = 3)
    {
        while (target != null)
        {
            Vector3 dir = target.position - transform.position;

            if (dir.magnitude <= attackRange)
            {
                myAnim.SetBool("IsMoving", false);

                reached?.Invoke();
                yield return new WaitForSeconds(attackDelay);
            }

            yield return null;
        }
    }
}

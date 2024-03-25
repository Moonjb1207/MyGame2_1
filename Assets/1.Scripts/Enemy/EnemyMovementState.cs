using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementState : EnemyState
{
    public NavMeshAgent agent;

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        agent.speed = enemy.data.moveSpeed;
    }

    public override void EnterState()
    {
        if(agent != null)
            agent.enabled = true;
    }

    public void ChangeMoveSpeed(float v)
    {
        agent.speed = enemy.data.moveSpeed * v;
    }

    public override void UpdateState()
    {
        curDelay += Time.deltaTime;

        if (enemy.target == null)
            return;
        if (enemy.dead)
            agent.enabled = false;

        float distance = (enemy.target.position - transform.position).magnitude;
        //공격 범위보다 가까우면
        if (distance <= enemy.data.attackRange)
        {
            Vector3 lookPoint = new Vector3(enemy.target.position.x, this.transform.position.y, enemy.target.position.z);
            bodyTr.transform.LookAt(lookPoint);

            myAnim.SetBool("IsMoving", false);
            agent.velocity = Vector3.zero;
            agent.isStopped = true;

            enemy.NextState(enemy.attackState);
            return;
        }
        else if (distance > enemy.data.attackRange && !myAnim.GetBool("IsAttacking"))//공격 범위보다 멂
        {
            Vector2 forward = new Vector2(transform.position.z, transform.position.x);
            Vector2 steeringTarget = new Vector2(agent.steeringTarget.z, agent.steeringTarget.x);

            //방향을 구한 뒤, 역함수로 각을 구한다.
            Vector2 dir = steeringTarget - forward;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            //방향 적용
            bodyTr.transform.eulerAngles = Vector3.up * angle;

            if (agent.SetDestination(enemy.target.position))
            {
                myAnim.SetBool("IsMoving", true);
                agent.isStopped = false;
                //가속도로 제외하여 유닛 이동 처리
                agent.velocity = agent.desiredVelocity.normalized * agent.speed;
            }
        }
    }
}

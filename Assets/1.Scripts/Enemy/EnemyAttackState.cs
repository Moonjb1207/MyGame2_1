using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public override void EnterState()
    {
        
    }

    //���� �ڵ��� ���� - ���� ���� �ӵ��� ���� ó���� ������� �ʾҽ��ϴ�.
    public override void UpdateState()
    {
        curDelay += Time.deltaTime;

        if(!myAnim.GetBool("IsAttacking") && curDelay >= enemy.data.atkSpeed)
        {
            enemy.OnAttack();
            curDelay = 0;
            enemy.NextState(enemy.movementState);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    public float hp; // ü��
    public float moveSpeed; //�̵� �ӵ�

    public float damage; //���ݷ�
    public float attackRange; //���� ����
    public float atkSpeed; //���� �ӵ�
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    public float hp; // 체력
    public float moveSpeed; //이동 속도

    public float damage; //공격력
    public float attackRange; //공격 범위
    public float atkSpeed; //공격 속도
}
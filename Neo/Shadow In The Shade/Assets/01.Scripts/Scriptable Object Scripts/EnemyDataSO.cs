using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/DataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("적 이름")]
    public string enemyName;
    [Header("적 프리팹")]
    public GameObject prefab;
    [Header("적 풀 모노 스크립트")]
    public PoolableMono poolPrefab;

    public int hitNum;
    
    [field: SerializeField, Header("적의 최대 체력")]
    public int maxHealth { get; set; } = 3;

    [field: SerializeField, Header("적의 공격력")]
    public float damage { get; set; } = 1;

    [field: SerializeField, Header("적의 경험치")]
    public float exprencepoint { get; set; } = 10;



}
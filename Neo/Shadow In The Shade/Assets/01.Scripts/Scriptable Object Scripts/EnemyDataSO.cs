using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Enemy/DataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("�� �̸�")]
    public string enemyName;
    [Header("�� ������")]
    public GameObject prefab;
    [Header("�� Ǯ ��� ��ũ��Ʈ")]
    public PoolableMono poolPrefab;

    public int hitNum;
    
    [field: SerializeField, Header("���� �ִ� ü��")]
    public int maxHealth { get; set; } = 3;

    [field: SerializeField, Header("���� ���ݷ�")]
    public float damage { get; set; } = 1;

    [field: SerializeField, Header("���� ����ġ")]
    public float exprencepoint { get; set; } = 10;



}
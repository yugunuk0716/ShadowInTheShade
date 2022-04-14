using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public int phaseCount = 0;

    public EnemyDataSO data;

    private void Start()
    {
        Enemy enemy = PoolManager.Instance.Pop(data.enemyName) as Enemy;

        enemy.transform.position = this.transform.position;
        enemy.enemyData = data;
    }



}

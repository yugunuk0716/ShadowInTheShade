using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public EnemyType enemyType;


    private void Start()
    {
        Enemy enemy = PoolManager.Instance.Pop(enemyType.ToString()) as Enemy;

        enemy.transform.position = this.transform.position;
    }


}

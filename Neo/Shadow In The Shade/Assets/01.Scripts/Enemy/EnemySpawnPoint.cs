using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public int phaseCount = 0;

    public EnemyDataSO data;

    public bool isSpawned = false;

    private void Start()
    {
       
    }


    public void Spawn()
    {
        isSpawned = true;
        Enemy enemy = PoolManager.Instance.Pop(data.enemyName) as Enemy;

        StageManager.Instance.curStageEnemys.Add(enemy);
        enemy.transform.position = this.transform.position;
        enemy.enemyData = data;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 1f);
            Gizmos.color = Color.white;
        }
    }
#endif
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public int phaseCount = 0;

    public Animator anima;
    private Animator Anim
    {
        get 
        {
            if (anima == null)
                anima = GetComponent<Animator>();
            return anima; 
        }
        set 
        {
            anima = value; 
        }

    }

    private SpriteRenderer sr;

    public EnemyDataSO data;
    private Enemy enemy;

    public bool isSpawned = false;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Spawn()
    {
      
        isSpawned = true;
        if(enemy != null)
        {
            enemy.transform.position = this.transform.position;
            enemy.enemyData = data;
        }
        sr.enabled = false;
    }

    public void StartSpawn()
    {
        Anim.SetTrigger("spawn");
        enemy = PoolManager.Instance.Pop(data.enemyName) as Enemy;

        StageManager.Instance.curStageEnemys.Add(enemy);
    }

    private void ResetSpawner()
    {
        sr.enabled = true;
        Anim.ResetTrigger("spawn");
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

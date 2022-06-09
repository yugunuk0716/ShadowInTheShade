using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public int phaseCount = 0;
    public bool isElite = false;


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
    private List<EnemyDataSO> canEliteList = new List<EnemyDataSO>();
    private Enemy enemy;

    public bool isSpawned = false;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        foreach (EnemyDataSO enemyDataSO in GameManager.Instance.enemyList.enemyList)
        {
            if (enemyDataSO.canChangeElite)
            {
                canEliteList.Add(enemyDataSO);
            }
        }
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
        
        if (isElite)
        {
            
            enemy = PoolManager.Instance.Pop(canEliteList[Random.Range(0, canEliteList.Count)].enemyName) as Enemy;
            enemy.SetElite();
        }
        else
        {
            enemy = PoolManager.Instance.Pop(data.enemyName) as Enemy;
            enemy.SetNomal();
        }

        StageManager.Instance.curStageEnemys.Add(enemy);

        Anim.SetTrigger("spawn");
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

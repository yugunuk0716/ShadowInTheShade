using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> _enemyList;
    [SerializeField]
    private int _count = 10;
    [SerializeField]
    private float _delay = 0.8f;


    //private void Start()
    //{
    //    StartCoroutine(SpawnCoroutine());
    //}


    //IEnumerator SpawnCoroutine()
    //{
    //    while (_count > 0)
    //    {
    //        int randomIndex = Random.Range(0, _enemyList.Count);
    //        Vector2 randomPos = Random.insideUnitCircle;

    //        Enemy spawnedEnemy = Instantiate(_enemyList[randomIndex]);
    //        spawnedEnemy.transform.position = transform.position + (Vector3)randomPos;
    //        spawnedEnemy.OnDie.AddListener(() => 
    //        {
    //            StageManager.Instance._enemys.Remove(spawnedEnemy); 
    //            if (StageManager.Instance._enemys.Count <= 0)
    //            {
    //                UIManager.Instance.clearPanel.gameObject.SetActive(true);
    //            }
    //        });
    //        StageManager.Instance._enemys.Add(spawnedEnemy);

            
    //        _count--;
    //        yield return new WaitForSeconds(_delay);
    //    }
    //}

    

}

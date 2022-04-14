using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("EnemyManager");
                obj.AddComponent<EnemyManager>();
                instance = obj.GetComponent<EnemyManager>();
            }

            return instance;
        }
    }

    public List<Enemy> enemyList = new List<Enemy>();


}

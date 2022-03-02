using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager instance;
    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("PoolManager");
                obj.AddComponent<PoolManager>();
                instance = obj.GetComponent<PoolManager>();
            }

            return instance;
        }
    }
    const int START_SIZE = 5;
    

    

    public Pool<T> CreatePool<T>(GameObject poolablePrefab, int poolSize = START_SIZE) where T : MonoBehaviour, IResettable
    {
        if(poolablePrefab == null)
        {
            print("ºö");
            return null;
        }
        return new Pool<T>(new PrefabFactory<T>(poolablePrefab), poolSize);
    }

   
}

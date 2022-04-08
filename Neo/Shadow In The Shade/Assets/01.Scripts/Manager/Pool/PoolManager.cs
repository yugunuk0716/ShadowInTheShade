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

    private Dictionary<string, Pool<PoolableMono>> pools = new Dictionary<string, Pool<PoolableMono>>();



    public void CreatePool(PoolableMono prefab, string name = null)
    {
        if (name == null)
            name = prefab.gameObject.name;
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, transform);
        pools.Add(name, pool);
    }

    

    public PoolableMono Pop(string prefabName)
    {
        if (!pools.ContainsKey(prefabName))
        {
            print(prefabName);
            Debug.LogError("Prefab doesnt exist on pool");
            return null;
        }

        PoolableMono item = pools[prefabName].Pop();
        item.Reset();
        return item;
    }



    public void Push(PoolableMono obj)
    {
        print($"{obj.name} ∏±∏Æ¡Ó");
        pools[obj.name].Push(obj);
        obj.transform.SetParent(this.transform);
    }




}

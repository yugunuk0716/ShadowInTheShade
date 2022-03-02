using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private static DamageManager instance;
    public static DamageManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("GameManager");
                obj.AddComponent<DamageManager>();
                instance = obj.GetComponent<DamageManager>();
            }

            return instance;
        }
    }

    public GameObject _damagePrefab;
    private Pool<DamagePopup> _damagePool = null;
    private bool isDeleted = false;
    private bool isCreated = false;
    public Pool<DamagePopup> DamagePool
    {
        get 
        {
            if(!isCreated)
            {
                isCreated = true;
                _damagePool = PoolManager.Instance.CreatePool<DamagePopup>(_damagePrefab);
            }

            return _damagePool;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDeleted)
                return;
            isDeleted = true;
            DamagePopup dPopup = DamagePool?.Allocate();

            dPopup?.gameObject.SetActive(true);
            dPopup?.SetText(10, transform.position + new Vector3(0, 0.5f, 0), false);
            isDeleted = false;
        }
    }

    public void Log(string text)
    {
        print(text);
    }
}

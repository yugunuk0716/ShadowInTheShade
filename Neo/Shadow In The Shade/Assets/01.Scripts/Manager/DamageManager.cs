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
                GameObject obj = new GameObject("DamageManager");
                obj.AddComponent<DamageManager>();
                instance = obj.GetComponent<DamageManager>();
            }

            return instance;
        }
    }

    public GameObject _damagePrefab;
    private bool isCreated = false;
    
    private void Awake()
    {
        _damagePrefab = Resources.Load<GameObject>("Prefabs/DamagePopup");
        instance = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            //DamagePopup dPopup = DamagePool?.Allocate();

            //dPopup?.gameObject.SetActive(true);
            //dPopup?.SetText(10, transform.position + new Vector3(0, 0.5f, 0), false);
        }
    }

    public void Log(string text)
    {
        print(text);
    }
}

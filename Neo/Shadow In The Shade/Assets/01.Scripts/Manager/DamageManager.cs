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

    
    private void Awake()
    {
        instance = this;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;

            dPopup?.gameObject.SetActive(true);
            dPopup?.SetText(10, transform.position + new Vector3(0.5f, 0.5f, 0), false);
        }
    }

    
}

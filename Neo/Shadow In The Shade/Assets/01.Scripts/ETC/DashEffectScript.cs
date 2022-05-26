using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffectScript : PoolableMono
{

    public List<GameObject> bases = new List<GameObject> ();

    public override void Reset()
    {
        foreach (GameObject item in bases)
        {
            item.transform.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
            item.transform.localScale = Vector3.one;
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

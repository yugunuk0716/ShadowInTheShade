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
            item.transform.localScale = Vector3.one;
        }
    }


    void Start()
    {
        GameManager.Instance.onPlayerChangeType.AddListener(() =>
        {
            if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow))
            {
                PoolManager.Instance.Push(this);
            }
        });
    }

    void Update()
    {
        
    }
}

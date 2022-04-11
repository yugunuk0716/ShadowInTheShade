using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Die_Smong : MonoBehaviour, IState
{
    SmongParticle obj;

    [System.Obsolete]
    public void OnEnter()
    {
        obj = PoolManager.Instance.Pop("Smong Die Effect") as SmongParticle;
        print(this.transform.position);
        obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, - 5f);

        
    }

    public void OnEnd()
    {
       
    }

   

}

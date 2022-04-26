using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticle : PoolableMono
{
    public ParticleSystemRenderer myRend;

    private void Awake()
    {
        myRend = GetComponent<ParticleSystemRenderer>();
        GameManager.Instance.OnPlayerChangeType.AddListener(() =>
        {
            
            myRend.enabled = !PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates);
           
        });
    }



    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }


    public override void Reset()
    {
        //myRend.enabled = true;
    }

}

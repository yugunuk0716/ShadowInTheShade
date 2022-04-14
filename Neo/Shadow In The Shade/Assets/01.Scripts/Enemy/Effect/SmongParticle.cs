using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmongParticle : PoolableMono
{

    public ParticleSystemRenderer myRend;

    private void Awake()
    {
        myRend = GetComponent<ParticleSystemRenderer>();
        GameManager.Instance.onPlayerChangeType.AddListener(() =>
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

    }

    
   
}

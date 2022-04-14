using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmongParticle : PoolableMono
{

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }

    
   
}

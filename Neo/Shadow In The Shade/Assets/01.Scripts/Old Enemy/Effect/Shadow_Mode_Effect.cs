using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_Mode_Effect : PoolableMono
{

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PushInPool()
    {
        PoolManager.Instance.Push(this);
    }

  
    public override void Reset()
    {
        anim.Rebind();
    }

}

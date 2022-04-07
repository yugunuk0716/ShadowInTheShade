using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : PoolableMono
{
    private Rigidbody2D rigid;
    public Rigidbody2D Rigid
    {
        get
        {
            if (rigid == null)
                rigid = GetComponent<Rigidbody2D>();
            return rigid;
        }
    }
    

    public override void Reset()
    {

    }

}

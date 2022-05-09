using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : PoolableMono
{
    private LineRenderer lr;
    public LineRenderer Lr 
    {
        get
        {
            if (lr == null)
                lr = GetComponent<LineRenderer>();
            return lr;
        }

    }

    public override void Reset()
    {
        Lr.widthMultiplier = 0.5f;
    }
}

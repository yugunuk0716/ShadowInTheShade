using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlime : Enemy
{
    private Idle_Patrol idle;

    protected override void Awake()
    {
        idle = GetComponent<Idle_Patrol>();
        dicState[State.Default] = idle;
        base.Awake();
    }

    protected override IEnumerator LifeTime()
    {
        return base.LifeTime();
    }

}

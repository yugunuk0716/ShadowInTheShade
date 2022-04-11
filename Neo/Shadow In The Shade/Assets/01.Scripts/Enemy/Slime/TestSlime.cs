using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlime : Enemy
{
    private Idle_Patrol idle;

    private void Awake()
    {
        idle = GetComponent<Idle_Patrol>();
        dicState[State.Default] = idle;

    }

    protected override IEnumerator LifeTime()
    {
        return base.LifeTime();
    }

}

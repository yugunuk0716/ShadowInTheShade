using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSlime : Enemy
{
    private Idle_Move idle;

    private void Awake()
    {
        idle = GetComponent<Idle_Move>();
        dicState[State.Default] = idle;

    }

    protected override IEnumerator LifeTime()
    {
        return base.LifeTime();
    }

}

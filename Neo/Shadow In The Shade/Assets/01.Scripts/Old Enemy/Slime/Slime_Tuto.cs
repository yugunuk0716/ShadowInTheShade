using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Slime_Tuto

public class Slime_Tuto : OldEnemy
{
    private SpriteRenderer sr;

    protected override void Awake()
    {
        dicState[OldEnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();

        sr = GetComponentInChildren<SpriteRenderer>();

        


        // Á×À½
        dicState[OldEnemyState.Die] = gameObject.AddComponent<Die_Default>();

        originColor = sr.color;
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }


    protected override void SetDefaultState(OldEnemyState state)
    {
        base.SetDefaultState(state);
    }

    protected override void SetState(OldEnemyState state)
    {
        base.SetState(state);
    }

    protected override void PlayState(OldEnemyState state)
    {
        base.PlayState(state);
    }

    protected override IEnumerator LifeTime()
    {
        yield return null;
        while (true)
        {
            if (IsHit)
            {
                yield return null;
                continue;
            }

            yield return base.LifeTime();
        }
    }

    public override void GetHit(float damage, int objNum)
    {
        if (isAttack)
            return;
        base.GetHit(damage, objNum);
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    //public override void SetDisable()
    //{
    //    StopCoroutine(lifeTime);
    //    base.SetDisable();
    //}

    public override IEnumerator Dead()
    {
        return base.Dead();
    }

    public override void Reset()
    {
        base.Reset();
    }


}

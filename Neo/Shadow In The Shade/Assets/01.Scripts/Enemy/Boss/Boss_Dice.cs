using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Dice : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void SetDefaultState(State state)
    {
        base.SetDefaultState(state);
    }

    protected override void SetState(State state)
    {
        base.SetState(state);
    }

    protected override void PlayState(State state)
    {
        base.PlayState(state);
    }

    protected override IEnumerator LifeTime()
    {
        return base.LifeTime();
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    public override void GetHit(int damage)
    {
        base.GetHit(damage);
    }

    public override IEnumerator Dead()
    {
        return base.Dead();
    }



    public override void Reset()
    {
        OnReset?.Invoke();
        currHP = enemyData.maxHealth;
        Anim.ResetTrigger("isDie");
        EnemyManager.Instance.enemyList.Remove(this);
        currentState = State.Default;
        isDie = false;
        isAttack = false;

    }




}

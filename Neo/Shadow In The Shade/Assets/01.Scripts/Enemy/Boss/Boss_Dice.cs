using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Dice : Enemy
{
    private readonly float attackDistance = 0f;

    Attack_Dice attack;

    protected override void Awake()
    {

        dicState[State.Default] = gameObject.AddComponent<Idle_Dice>();


        //chase = gameObject.AddComponent<Move_Chase>();
        //chase.speed = 2f;

        //dicState[State.Move] = chase;

        attack = gameObject.AddComponent<Attack_Dice>();

        dicState[State.Attack] = attack;

        dicState[State.Die] = gameObject.AddComponent<Die_Default>();
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

        while (true)
        {
            print($"{IsDisarmed}, {isAttack}");
            print("asd1");
            if (!isAttack)
            {
                print("asd2");
                if (GameManager.Instance != null)
                {

                    if ((GameManager.Instance.player.position - transform.position).magnitude < attackDistance)
                    {
                        SetState(State.Attack);
                    }

                    else
                    {
                        print("asd");
                        SetState(State.Default);
                    }
                }

            }
            yield return null;
            //return base.LifeTime();
        }
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

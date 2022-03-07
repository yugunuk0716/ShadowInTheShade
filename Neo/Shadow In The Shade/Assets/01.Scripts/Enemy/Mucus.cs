using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : Enemy
{

    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();
    private int currentPhase = 0;
    private bool isPhaseEnd = false;
    private bool isInvincible = false;

    private int currentWaitTime = 0;

    private Coroutine phaseRoutine = null;
    private Coroutine attackRoutine = null;

    private State_Chase chase = null;
    private State_Attack attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);

    private void Awake()
    {
        dicState[State.Default] = gameObject.AddComponent<State_Default>();



        // 이동
        chase = GetComponent<State_Chase>();
        chase.speed = 2f;


        dicState[State.Move] = chase;

        // 공격
        attack = gameObject.AddComponent<State_Attack>();

        dicState[State.Attack] = attack;

        // 죽음
        dicState[State.Die] = gameObject.AddComponent<State_Die>();

        currentPhase = 0;
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
        yield return null;
        while (true)
        {
            

            yield return base.LifeTime();
        }
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    protected override void CheckHp()
    {
        base.CheckHp();
    }

    public override void SetDisable()
    {
        base.SetDisable();
    }
}

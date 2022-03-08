using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : Enemy
{

    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();
    private int currentPhase = 0;
    private bool isPhaseEnd = false;
    private bool isInvincible = false;

    private bool isAttack = false;

    private int currentWaitTime = 0;
    private float attackDistance = 1f;
    private float chaseDistance = 5f;

    private Coroutine phaseRoutine = null;
    private Coroutine attackRoutine = null;

    private Move_Chase chase = null;
    private Attack_Mucus attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);

    private void Awake()
    {
        dicState[State.Default] = gameObject.AddComponent<State_Default>();



        // 이동
        chase = GetComponent<Move_Chase>();
        chase.speed = 2f;


        dicState[State.Move] = chase;

        // 공격
        attack = gameObject.AddComponent<Attack_Mucus>();

        dicState[State.Attack] = attack;

        // 죽음
        dicState[State.Die] = gameObject.AddComponent<Die_Default>();

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
            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);
            if (dist < chaseDistance && dist > attackDistance)
            {
                dicState[State.Move].OnEnter();
                //print("?");
            }
            else
            {
                dicState[State.Move].OnEnd();
            }
            if(dist < attackDistance && !isAttack)
            {
                GameManager.Instance.onStateEnter.AddListener(() => 
                {
                    if (isAttack)
                        return;
                    isAttack = true; 
                });
                GameManager.Instance.onStateEnd.AddListener(() =>
                {
                    if (!isAttack)
                        return;
                    isAttack = false;
                });
                
                dicState[State.Attack].OnEnter();
                
            }
            
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
        StopCoroutine(lifeTime);
        base.SetDisable();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0.4f, 0.3f);
        Gizmos.DrawSphere(transform.position, attackDistance * 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Beaker : Enemy, ITacklable
{
    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();


    private readonly float attackDistance = 2f;
    private readonly float chaseDistance = 5f;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);


    private int reincarnationIdx = 0;

    protected override void Awake()
    {
        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();


        chase = gameObject.AddComponent<Move_Chase>();
        chase.speed = 2f;

        dicState[EnemyState.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Default>();
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.onPlayerTypeChanged.AddListener(() =>
        {
            isAttack = false;
            Anim.SetBool("isTackle", false);
        });
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public void SetTackle(bool on)
    {
        isAttack = on;
    }

    public void SetAttack()
    {
        attack.TackleEnd();
    }


    protected override void SetDefaultState(EnemyState state)
    {
        base.SetDefaultState(state);
    }

    protected override void SetState(EnemyState state)
    {
        base.SetState(state);
    }

    protected override void PlayState(EnemyState state)
    {
        base.PlayState(state);
    }

    protected override IEnumerator LifeTime()
    {
        yield return null;
        while (true)
        {
            //if (!Anim.GetBool("isReincarnation"))
            //    continue;


            if (IsHit)
            {
                yield return null;
                continue;
            }

            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);

            if (!Anim.GetBool("isReincarnation") && !isAttack)
            {
                if (dist < chaseDistance )
                {
                    if (dist < chaseDistance && dist > attackDistance)
                    {
                        SetState(EnemyState.Move);
                    }
                 
                    if (dist < attackDistance && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(EnemyState.Attack);
                    }
                }
                else if (!isDie)
                {
                    SetState(EnemyState.Default);
                }
            }

            yield return base.LifeTime();
        }
    }


    public override void GetHit(float damage)
    {

        base.GetHit(damage);
    }

    protected override void CheckHP()
    {
        if (!Anim.GetBool("isReincarnation") && currHP <= 0f)
        {
            chase.speed = 0f;
            currHP = enemyData.maxHealth / 2;
            IsHit = false;
            Anim.SetBool("isReincarnation", true);
            return;
        }

        base.CheckHP();
    }

    public override IEnumerator Dead()
    {
        chase.speed = 0f;
        return base.Dead();
    }


    public void WaitingReincarnation()
    {
        isAttack = true;
        reincarnationIdx++;
        if(reincarnationIdx >= 10)
        {
            Reincarnation();
        }
    }

    public void Reincarnation()
    {
        chase.speed = 0f;
        IsHit = false;
        reincarnationIdx = 0;
        lastHitTime = Time.time;
        Anim.SetBool("isReincarnation", false);
    }

    public void ReincarnationEnd()
    {
        isAttack = false;
        gameObject.layer = 6;
        dicState[EnemyState.Move].OnEnd();
        chase.speed = 3f;
        //dicState[State.Move].OnEnter();
    }

    public override void Reset()
    {
        base.Reset();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.color = Color.white;
        }
    }
#endif
}

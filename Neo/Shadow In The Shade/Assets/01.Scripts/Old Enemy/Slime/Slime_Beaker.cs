using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Beaker : OldEnemy, ITacklable
{
    private readonly float attackDistance = 2f;
    private readonly float chaseDistance = 5f;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;
    private Idle_Patrol idle = null;

    private int reincarnationIdx = 0;

    

    protected override void Awake()
    {
        idle = gameObject.AddComponent<Idle_Patrol>();
        dicState[OldEnemyState.Default] = idle;


        chase = gameObject.AddComponent<Move_Chase>();
        speed = 2f;

        dicState[OldEnemyState.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[OldEnemyState.Attack] = attack;

        dicState[OldEnemyState.Die] = gameObject.AddComponent<Die_Default>();
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

            if (Anim.GetBool("isReincarnation"))
            {
                SetAttack(false);
                yield return null;
                continue;
            }

            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);

            if (!Anim.GetBool("isReincarnation") && !isAttack)
            {
                if (dist < chaseDistance )
                {

                    if (dist < attackDistance && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(OldEnemyState.Attack);
                        attack.canAttack = true;
                        chase.canTrace = false;
                        idle.canMove = false;
                    }
                    else if (dist < chaseDistance)
                    {
                        chase.canTrace = true;
                        SetAttack(true);
                        SetState(OldEnemyState.Move);
                    }
                 
                   
                }
                else if (!isDie)
                {
                    SetState(OldEnemyState.Default);
                    idle.canMove = true;
                }
            }
            else
            {
                chase.canTrace = false;
            }

            yield return base.LifeTime();
        }
    }

    public override void GetHit(float damage, int objNum)
    {
        attack.TackleEnd();
        base.GetHit(damage, objNum);
    }

    protected override void CheckHP()
    {
        if (!Anim.GetBool("isReincarnation") && currHP <= 0f)
        {
            chase.speed = 0f;
            currHP = enemyData.maxHealth / 2;
            IsHit = false;
            chase.canTrace = false;
            destinationSetter.target = null;
            SetAttack(false);
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
        Anim.SetBool("isReincarnation", false);
    }

    public void ReincarnationEnd()
    {
        isAttack = false;
        dicState[OldEnemyState.Move].OnEnd();
        chase.speed = 3f;
        //SetAttack(true);
        //dicState[State.Move].OnEnter();
    }

    public override void KnockBack(Vector2 direction, float power, float duration)
    {
        base.KnockBack(direction, power, duration);
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

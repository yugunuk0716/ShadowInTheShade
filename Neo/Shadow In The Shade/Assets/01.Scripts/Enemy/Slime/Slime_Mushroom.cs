using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Mushroom : Enemy, IDamagable
{
    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();

    private SpriteRenderer sr;

    private readonly float attackDistance = 6f;
    private readonly float chaseDistance = 5f;

    private Move_Chase chase = null;
    private Attack_Mushroom attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);

    protected override void Awake()
    {
        dicState[State.Default] = gameObject.AddComponent<Idle_Patrol>();

        sr = GetComponentInChildren<SpriteRenderer>();

        chase = gameObject.AddComponent<Move_Chase>();
        chase.speed = -2f;

        dicState[State.Move] = chase;

        attack = gameObject.AddComponent<Attack_Mushroom>();

        dicState[State.Attack] = attack;

        dicState[State.Die] = gameObject.AddComponent<Die_Default>();

        attackCool *= 2;

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

    public void SetTackle(bool on)
    {
        isAttack = on;
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
            }
            else
            {
                dicState[State.Move].OnEnd();
            }
            if (dist < attackDistance && lastAttackTime + attackCool < Time.time)
            {
                lastAttackTime = Time.time;
                dicState[State.Move].OnEnd();
                dicState[State.Attack].OnEnter();
            }

            yield return base.LifeTime();
        }
    }


    public override void GetHit(int damage)
    {
        base.GetHit(damage);
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    public override IEnumerator Dead()
    {
        chase.speed = 0f;
        return base.Dead();
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

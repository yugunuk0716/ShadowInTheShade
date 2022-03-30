using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmongSlime : Enemy, ITacklable
{
    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();

    private SpriteRenderer sr;

    private float attackDistance = 1f;
    private float chaseDistance = 5f;

    private Coroutine phaseRoutine = null;
    private Coroutine attackRoutine = null;

    [Range(0f, 1f)]
    [SerializeField]
    private float spriteAlpha;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);

    private void Awake()
    {
        dicState[State.Default] = gameObject.AddComponent<State_Default>();

        sr = GetComponentInChildren<SpriteRenderer>();

        chase = GetComponent<Move_Chase>();
        chase.speed = 2f;

        dicState[State.Move] = chase;

        attack = gameObject.AddComponent<Attack_Tackle>();

        dicState[State.Attack] = attack;

        dicState[State.Die] = gameObject.AddComponent<Die_Default>();

    }

    public void SetTackle(bool on)
    {
        isAttack = on;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetHit(1);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            currHP++;
            CheckHp();
        }
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
            if (dist < attackDistance && !isAttack)
            {
                dicState[State.Move].OnEnd();
                dicState[State.Attack].OnEnter();
            }

            yield return base.LifeTime();
        }
    }

    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    public override void GetHit(int damage)
    {
        base.GetHit(damage);
    }

    protected override void CheckHp()
    {
        base.CheckHp();
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

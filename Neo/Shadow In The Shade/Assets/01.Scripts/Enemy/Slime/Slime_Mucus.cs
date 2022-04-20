using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Mucus : Enemy
{

    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();

    private SpriteRenderer sr;

    private readonly float attackDistance = 1f;
    private readonly float chaseDistance = 5f;



    [Range(0f, 1f)]
    [SerializeField]
    private float spriteAlpha;
    private Color originColor;
    private Color attachedColor;

    private Move_Chase chase = null;
    private Attack_Mucus attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);

    protected override void Awake()
    {
        dicState[State.Default] = gameObject.AddComponent<Idle_Patrol>();

        sr = GetComponentInChildren<SpriteRenderer>();

        // 이동
        chase = gameObject.AddComponent<Move_Chase>();
        chase.speed = 2f;


        dicState[State.Move] = chase;

        // 공격
        attack = gameObject.AddComponent<Attack_Mucus>();

        dicState[State.Attack] = attack;

        // 죽음
        dicState[State.Die] = gameObject.AddComponent<Die_Default>();

        originColor = sr.color;
        attachedColor = new Color(originColor.r, originColor.g, originColor.b, spriteAlpha);
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

    public void SetMucus(bool on)
    {
        GameManager.Instance.isInvincible = on;
        sr.color = on ? attachedColor : originColor;
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
            if (IsHit)
            {
                yield return null;
                continue;
            }


            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);
            if (dist < chaseDistance)
            {
                if (dist > attackDistance)
                {
                    SetState(State.Move);
                }


                if (dist < attackDistance && !isAttack && !GameManager.Instance.isInvincible)
                {
                    SetState(State.Attack);
                }
            }
            else
            {
                SetState(State.Default);
            }

            yield return base.LifeTime();
        }
    }

    public override void GetHit(int damage)
    {
        if (isAttack)
            return;
        base.GetHit(damage);
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

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Mucus : Enemy
{
    private SpriteRenderer sr;

    private readonly float attackDistance = 0.5f;
    private readonly float chaseDistance = 5f;

    [Range(0f, 1f)]
    [SerializeField]
    private float spriteAlpha;
    private Color attachedColor;

    private Move_Chase chase = null;
    private Attack_Mucus attack = null;

    protected override void Awake()
    {
        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();

        sr = GetComponentInChildren<SpriteRenderer>();

        // 이동
        chase = gameObject.AddComponent<Move_Chase>();
        chase.speed = 2f;


        dicState[EnemyState.Move] = chase;

        // 공격
        attack = gameObject.AddComponent<Attack_Mucus>();

        dicState[EnemyState.Attack] = attack;

        // 죽음
        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Default>();

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
                    SetState(EnemyState.Move);
                }


                if (dist < attackDistance && !isAttack && !GameManager.Instance.isInvincible)
                {
                    SetState(EnemyState.Attack);
                }
            }
            else if(!attack.isStateEnter)
            {
                SetState(EnemyState.Default);
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

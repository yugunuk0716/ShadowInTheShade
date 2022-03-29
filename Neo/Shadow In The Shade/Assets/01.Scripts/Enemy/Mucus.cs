using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mucus : Enemy
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
    private Color originColor;
    private Color attachedColor;

    private Move_Chase chase = null;
    private Attack_Mucus attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);

    private void Awake()
    {
        dicState[State.Default] = gameObject.AddComponent<State_Default>();

        sr = GetComponentInChildren<SpriteRenderer>();

        // 이동
        chase = GetComponent<Move_Chase>();
        chase.speed = 2f;


        dicState[State.Move] = chase;

        // 공격
        attack = gameObject.AddComponent<Attack_Mucus>();

        dicState[State.Attack] = attack;

        // 죽음
        dicState[State.Die] = gameObject.AddComponent<Die_Default>();

        originColor = sr.color;
        attachedColor = new Color(originColor.r, originColor.g, originColor.b, spriteAlpha);
    }

    private void Start()
    {
        GameManager.Instance.onStateEnter.AddListener(() =>
        {
            if (isAttack)
                return;

            GameManager.Instance.isInvincible = true;
            sr.color = attachedColor;
            isAttack = true;
        });
        GameManager.Instance.onStateEnd.AddListener(() =>
        {
            if (!isAttack)
                return;
            GameManager.Instance.isInvincible = false;
            sr.color = originColor;
            isAttack = false;
        });
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
            if(dist < attackDistance && !isAttack && !GameManager.Instance.isInvincible)
            {
               
                
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

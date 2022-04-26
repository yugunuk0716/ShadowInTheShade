using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Bone : Enemy, ITacklable
{
    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();

    private SpriteRenderer sr;

    private readonly float attackDistance = 2f;
    private readonly float chaseDistance = 5f;
    private int hitCount = 0;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;

    int defaultDamage;
    readonly int damageIncreaseAmout = 2;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);

    protected override void Awake()
    {
        dicState[State.Default] = gameObject.AddComponent<Idle_Patrol>();

        sr = GetComponentInChildren<SpriteRenderer>();

        chase = gameObject.AddComponent<Move_Chase>();
        chase.speed = 2f;

        dicState[State.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[State.Attack] = attack;

        dicState[State.Die] = gameObject.AddComponent<Die_Default>();

        defaultDamage = enemyData.damage;
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

    public void SetAttack()
    {

        attack.TackleEnd();
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

            if (IsHit || isDie)
            {
                yield return null;
                continue;
            }

            if (!isAttack)
            {
                if (dist < chaseDistance)
                {
                    if (dist > attackDistance)
                    {
                        SetState(State.Move);
                    }

                    if (dist < attackDistance && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(State.Attack);
                    }
                }
                else
                {
                    SetState(State.Default);
                }
            }
          
            
            yield return base.LifeTime();
        }
    }


    public override void GetHit(int damage)
    {
        hitCount++;
        base.GetHit(damage);
    }

    protected override void CheckHP()
    {

        if (hitCount > 3)
        {
            Anim.SetTrigger("FinalArmored");
            enemyData.damage *= damageIncreaseAmout;
        }
        else if(hitCount > 1)
        {
            Anim.SetTrigger("Armored");
            enemyData.damage *= damageIncreaseAmout;
        }

        base.CheckHP();
    }

    public override IEnumerator Dead()
    {
        chase.speed = 0f;
        return base.Dead();
    }

   

    public override void Reset()
    {
        enemyData.damage = defaultDamage;
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
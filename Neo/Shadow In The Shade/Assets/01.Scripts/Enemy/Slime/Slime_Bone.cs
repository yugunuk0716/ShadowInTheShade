using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Bone : Enemy, ITacklable
{

    private readonly float attackDistance = 1f;
    private readonly float chaseDistance = 5f;
    private int hitCount = 0;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;
    private Idle_Patrol idle = null;

    float defaultDamage;
    readonly int damageIncreaseAmout = 2;



    protected override void Awake()
    {
        idle = gameObject.AddComponent<Idle_Patrol>();
        dicState[EnemyState.Default] = idle;

        chase = gameObject.AddComponent<Move_Chase>();
        speed = 2f;
        dicState[EnemyState.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Default>();

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
                    if (dist < attackDistance && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(EnemyState.Attack);
                        attack.canAttack = true;
                        chase.canTrace = false;
                        idle.canMove = false;
                    }
                    else if (dist < chaseDistance)
                    {
                        chase.canTrace = true;
                        SetState(EnemyState.Move);
                    }
                }
                else
                {
                    SetState(EnemyState.Default);
                    idle.canMove = true;
                }
            }
          
            
            yield return base.LifeTime();
        }
    }


    public override void GetHit(float damage)
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

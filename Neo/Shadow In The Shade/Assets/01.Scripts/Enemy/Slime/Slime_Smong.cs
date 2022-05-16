using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Smong : Enemy, ITacklable
{
    private readonly float attackDistance = 2f;
    private readonly float chaseDistance = 5f;


    private Idle_Patrol idle = null;
    private Move_Chase chase = null;
    private Attack_Tackle attack = null;

    protected override void Awake()
    {
        idle = gameObject.AddComponent<Idle_Patrol>();
        dicState[EnemyState.Default] = idle;

        chase = gameObject.AddComponent<Move_Chase>();
        speed = 2f;

        dicState[EnemyState.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Smong>();

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
            if (IsHit)
            {
                yield return null;
                continue;
            }

            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);

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
        attack.TackleEnd();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Mushroom : Enemy, IDamagable
{
    private readonly float attackDistance = 6f;
    private readonly float chaseDistance = 3f;

    private Move_Chase chase = null;
    private Attack_Mushroom attack = null;

   

    protected override void Awake()
    {
        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();

        chase = gameObject.AddComponent<Move_Chase>();
        chase.speed = -2f;

        dicState[EnemyState.Move] = chase;

        attack = gameObject.AddComponent<Attack_Mushroom>();

        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Default>();

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

            if(dist < chaseDistance)
            {
                if (dist > attackDistance)
                {
                    SetState(EnemyState.Move);
                }
                if (dist < attackDistance && lastAttackTime + attackCool < Time.time)
                {
                    lastAttackTime = Time.time;
                    SetState(EnemyState.Attack);
                }
            }
            else
            {
                SetState(EnemyState.Default);
            }

            

            yield return base.LifeTime();
        }
    }


    public override void GetHit(float damage, int objNum)
    {
        base.GetHit(damage, objNum);
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

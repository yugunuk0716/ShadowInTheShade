using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Smong : OldEnemy, ITacklable
{
    private readonly float attackDistance = 2f;
    private readonly float chaseDistance = 5f;


    private Idle_Patrol idle = null;
    private Move_Chase chase = null;
    private Attack_Tackle attack = null;

    protected override void Awake()
    {
        idle = gameObject.AddComponent<Idle_Patrol>();
        dicState[OldEnemyState.Default] = idle;

        chase = gameObject.AddComponent<Move_Chase>();
        speed = 2f;

        dicState[OldEnemyState.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[OldEnemyState.Attack] = attack;

        dicState[OldEnemyState.Die] = gameObject.AddComponent<Die_Smong>();

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

            float dist = Vector2.Distance(transform.position, GameManager.Instance.player.position);

            if (!isAttack)
            {
                if (dist < chaseDistance)
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
                        SetState(OldEnemyState.Move);
                    }
                }
                else
                {
                    SetState(OldEnemyState.Default);
                    idle.canMove = true;
                }
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

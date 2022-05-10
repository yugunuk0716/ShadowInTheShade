using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Fire : Enemy, IDamagable
{
    private readonly float chaseDistance = 5f;
    private readonly float attackDistance = 2f;

    private Move_Chase chase = null;
    private Attack_Fire attack = null;

    protected override void Awake()
    {
        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();


        chase = gameObject.AddComponent<Move_Chase>();

        dicState[EnemyState.Move] = chase;

        attack = gameObject.AddComponent<Attack_Fire>();

        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Default>();

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
                
                SetState(EnemyState.Move);
                

                if(dist < attackDistance)
                {
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

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    public override IEnumerator Dead()
    {
        //if (isDie.Equals(true))
        //{
        //    Anim.SetTrigger("isDie");
        //    yield return null;
        //    this.gameObject.SetActive(false);
        //    chase.speed = 0f;
        //}
        yield return base.Dead();
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
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.color = Color.white;
        }
    }
#endif
}

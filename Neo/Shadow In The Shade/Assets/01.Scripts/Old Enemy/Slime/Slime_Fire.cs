using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Fire : OldEnemy, IDamagable
{
    private readonly float chaseDistance = 5f;
    private readonly float attackDistance = 1f;

    private Move_Chase chase = null;
    private Attack_Fire attack = null;

    protected override void Awake()
    {
        dicState[OldEnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();


        chase = gameObject.AddComponent<Move_Chase>();

        dicState[OldEnemyState.Move] = chase;

        attack = gameObject.AddComponent<Attack_Fire>();

        dicState[OldEnemyState.Attack] = attack;

        dicState[OldEnemyState.Die] = gameObject.AddComponent<Die_Default>();

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


            if(dist < chaseDistance)
            {
                
                SetState(OldEnemyState.Move);
                

                if(dist < attackDistance)
                {
                    SetState(OldEnemyState.Attack);
                }

                
            }
            else
            {
                SetState(OldEnemyState.Default);
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

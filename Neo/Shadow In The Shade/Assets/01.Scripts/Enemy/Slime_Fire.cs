using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Fire : Enemy, IDamagable
{
    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();

    private readonly float attackDistance = 1f;
    private readonly float chaseDistance = 5f;

    [Range(0f, 1f)]
    [SerializeField]
    private float spriteAlpha;

    private Move_Chase chase = null;
    private Attack_Fire attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);


    private void Awake()
    {
        dicState[State.Default] = gameObject.GetComponent<Idle_Patrol>();


        chase = GetComponent<Move_Chase>();
        chase.speed = 2f;

        dicState[State.Move] = chase;

        attack = gameObject.GetComponent<Attack_Fire>();

        dicState[State.Attack] = attack;

        dicState[State.Die] = gameObject.GetComponent<Die_Default>();

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
            }
            else
            {
                dicState[State.Move].OnEnd();
            }


            if (dist < attackDistance && !isAttack)
            {
                dicState[State.Move].OnEnd();
                dicState[State.Attack].OnEnter();
                
            }

            yield return base.LifeTime();
        }
    }


    public override void GetHit(int damage)
    {
        base.GetHit(damage);
    }

    protected override void CheckHP()
    {
        base.CheckHP();
    }

    public override IEnumerator Dead()
    {
        if (isDie.Equals(true))
        {
            Anim.SetTrigger("isDie");
            yield return null;
            this.gameObject.SetActive(false);
            chase.speed = 0f;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSlime : Enemy, IDamagable
{
    private List<PhaseInfo> phaseInfoList = new List<PhaseInfo>();

    private float chaseDistance = 5f;

    [Range(0f, 1f)]
    [SerializeField]
    private float spriteAlpha;

    private Move_Chase chase = null;
    private Attack_Tackle attack = null;

    private readonly WaitForSeconds halfSecWait = new WaitForSeconds(0.5f);
    private readonly WaitForSeconds oneSecWait = new WaitForSeconds(1f);
    private readonly WaitForSeconds threeSecWait = new WaitForSeconds(3f);


    private void Awake()
    {
        dicState[State.Default] = gameObject.GetComponent<State_Default>();


        chase = GetComponent<Move_Chase>();
        chase.speed = 2f;

        dicState[State.Move] = chase;

        attack = gameObject.GetComponentInChildren<Attack_Tackle>();

        dicState[State.Attack] = attack;

        dicState[State.Die] = gameObject.GetComponent<Die_Smong>();

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
            

            yield return base.LifeTime();
        }
    }


    public override void GetHit(int damage)
    {
        base.GetHit(damage);
    }

    protected override void CheckHp()
    {
        base.CheckHp();
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
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.color = Color.white;
        }
    }
#endif
}

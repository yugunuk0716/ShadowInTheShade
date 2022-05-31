using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Moss : Enemy
{
    private readonly float chaseDistance = 5f;

    private Attack_Moss attack = null;

    protected override void Awake()
    {
        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Patrol>();


        // °ø°Ý
        attack = gameObject.AddComponent<Attack_Moss>();

        dicState[EnemyState.Attack] = attack;

        // Á×À½
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
        while (true)
        {
            yield return null;
            
            SetState(EnemyState.Attack);
            SetState(EnemyState.Default);

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

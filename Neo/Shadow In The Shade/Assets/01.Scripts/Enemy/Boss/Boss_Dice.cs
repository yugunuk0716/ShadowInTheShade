using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DiceType
{
    Mk1,
    Mk2,
    Mk3,
}

public class Boss_Dice : Enemy
{

    public bool isAttacking = false;
    public bool isMoving = false;
    public DiceType diceType;

    private Attack_Dice attack;
    private Move_Dice moveDice;
    private  float lastMoveTime = 0f;
    private float moveCool = 4f;

    private readonly float attackDistance = 3f;
    private readonly float dist = 4f;
    

    protected override void Awake()
    {

        attackCool = 3f;

        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Dice>();

        moveDice = gameObject.AddComponent<Move_Dice>();
        dicState[EnemyState.Move] = moveDice;


        attack = gameObject.AddComponent<Attack_Dice>();
        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Dice>();
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


    protected override IEnumerator LifeTime()
    {

        while (true)
        {
            yield return new WaitUntil(() => !IsDisarmed);

            if (PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates))
            {
                Shadow_Mode_Effect sme = PoolManager.Instance.Pop("Shadow Purse") as Shadow_Mode_Effect;
                sme.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            }

            if (!isAttack)
            {
                if (GameManager.Instance != null)
                {

                    if ( !isMoving && (GameManager.Instance.player.position - transform.position).sqrMagnitude < Mathf.Pow(attackDistance, 2f) && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(EnemyState.Attack);
                    }

                    else if(!isAttacking && moveCool + lastMoveTime < Time.time)
                    {
                        //lastAttackTime = Time.time;
                        lastMoveTime = Time.time;
                        SetState(EnemyState.Move);
                    }

                    else if(!isAttacking && !isMoving)
                    {
                        SetState(EnemyState.Default);
                    }
                }

            }
            yield return new WaitForSeconds(.3f);
            //return base.LifeTime();
        }
    }

    public void SetCrashEnd()
    {
        isAttacking = false;
    }



    protected override void CheckHP()
    {
        if (currHP <= 0)
        {
            StopCoroutine(lifeTime);
            StageManager.Instance.curStageEnemys.Remove(this);
            isDie = true;
            StartCoroutine(Dead());
            OnDie?.Invoke();
            //SetDisable();
        }
    }
    public override void KnockBack(Vector2 direction, float power, float duration)
    {
        return;
    }

    public override void GetHit(float damage)
    {
        base.GetHit(damage);
    }

    public override void PushInPool()
    {
        SetState(EnemyState.Die);
        isAttacking = false;
        isMoving = false;
        attack.OnEnd();
        moveDice.OnEnd();
        Move.rigid.velocity = Vector2.zero;
        base.PushInPool();
    }

    public override IEnumerator Dead()
    {
       
        yield return base.Dead();
    }



    public override void Reset()
    {
        //OnReset?.Invoke();
        //currHP = enemyData.maxHealth;
        //Anim.ResetTrigger("isDie");
        //EnemyManager.Instance.enemyList.Remove(this);
        //currentState = State.Default;
        //isDie = false;
        //isAttack = false;
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
            Gizmos.DrawWireSphere(transform.position, dist);
            Gizmos.color = Color.white;
        }
    }
#endif


}

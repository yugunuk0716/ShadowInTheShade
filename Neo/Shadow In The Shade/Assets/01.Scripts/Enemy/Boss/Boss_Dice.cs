using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Dice : Enemy
{
    private readonly float attackDistance = 3f;
    private readonly float dist = 4f;
    public bool isAttacking = false;
    Attack_Dice attack;

    protected override void Awake()
    {

        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Dice>();


        attack = gameObject.AddComponent<Attack_Dice>();

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

                    if ((GameManager.Instance.player.position - transform.position).sqrMagnitude < Mathf.Pow(attackDistance, 2f) && attackCool + lastAttackTime < Time.time)
                    {
                        lastAttackTime = Time.time;
                        SetState(EnemyState.Attack);
                    }
                    else if(!isAttacking)
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
        base.CheckHP();
    }

    public override void GetHit(int damage)
    {
        base.GetHit(damage);
    }

    public override IEnumerator Dead()
    {
        yield return null;
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

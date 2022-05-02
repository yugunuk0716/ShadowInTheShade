using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Dice : Enemy
{
    private readonly float attackDistance = 2f;
    private readonly float dist = 4f;

    Attack_Dice attack;

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);


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

                    if ((GameManager.Instance.player.position - transform.position).magnitude < attackDistance)
                    {
                        SetState(EnemyState.Attack);
                    }

                    else
                    {
                        SetState(EnemyState.Default);
                    }
                }

            }
            yield return new WaitForSeconds(.3f);
            //return base.LifeTime();
        }
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

  
}

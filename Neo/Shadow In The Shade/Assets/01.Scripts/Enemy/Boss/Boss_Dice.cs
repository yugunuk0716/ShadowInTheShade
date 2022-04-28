using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boss_Dice : PoolableMono, IDamagable
{
    private readonly float attackDistance = 0f;

    Attack_Dice attack;

    private int currHP = 0;
    public int CurrHP
    {
        get
        {
            return currHP;
        }

        set
        {
            currHP = value;
            CheckHP();
        }
    }

    private bool isHit = false;
    public bool IsHit
    {
        get
        {
            return isHit;
        }
        set
        {
            isHit = value;
        }
    }

    private bool isDisarmed = false;
    public bool IsDisarmed
    {
        get
        {
            return isDisarmed;
        }
        set
        {
            if (!value)
                move.rigid.velocity = Vector2.zero;
            isDisarmed = value;
        }
    }

    [Space(10)]
    public bool isAttack = false;
    public bool isDie = false;

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }
    [field: SerializeField]
    public UnityEvent OnReset { get; set; }

    [HideInInspector]
    public AudioClip slimeHitClip;

    public AgentMove move;

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);


    protected  void Awake()
    {

        dicState[EnemyState.Default] = gameObject.AddComponent<Idle_Dice>();


        attack = gameObject.AddComponent<Attack_Dice>();

        dicState[EnemyState.Attack] = attack;

        dicState[EnemyState.Die] = gameObject.AddComponent<Die_Default>();

    }

    protected Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();

    protected void Start()
    {
        
    }

    protected void OnEnable()
    {
        
    }

    protected void SetDefaultState(EnemyState state)
    {
        
    }

    protected void SetState(EnemyState state)
    {
       
    }

    protected void PlayState(EnemyState state)
    {
        
    }

    protected IEnumerator LifeTime()
    {

        while (true)
        {
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
            yield return null;
            //return base.LifeTime();
        }
    }

    protected void CheckHP()
    {
        
    }

    public void GetHit(int damage)
    {
        
    }

    public IEnumerator Dead()
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

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        throw new System.NotImplementedException();
    }
}

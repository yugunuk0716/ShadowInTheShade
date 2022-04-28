using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PhaseInfo
{
    public int waitTime;
    public float hp;
}

public class Enemy : PoolableMono, IAgent, IDamagable
{
    protected enum State
    {
        Default,    // 아무것도 없는 상태
        Move,       // 움직일 때
        Attack,     // 공격할 때
        Die         // 죽을 때
    }

    public EnemyDataSO enemyData;

    protected int currHP = 0;
    public int CurrHP
    {
        get
        {
            return currHP;
        }

        set
        {
            print("나한테 왜그래요");
            currHP = value;
            CheckHP();
        }
    }

    [Space(10)]
    public bool isAttack = false;
    public bool isDie = false;
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

    protected float lastAttackTime = 0f;
    protected float attackCool = 1f;
    protected float hitCool = 0.5f;
    protected float lastHitTime = 0f;

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

    private Animator anim;
    public Animator Anim
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }

            return anim;
        }
    }

    private SpriteRenderer myRend;
    protected SpriteRenderer MyRend
    {
        get
        {
            if (myRend == null)
            {
                myRend = GetComponent<SpriteRenderer>();
            }

            return myRend;
        }
    }




    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }
    [field: SerializeField]
    public UnityEvent OnReset { get; set; }

    public AgentMove move;

    [HideInInspector]
    public AudioClip slimeHitClip;

    public bool isShadow = false;

    public float speed = 3f;

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);

    [SerializeField]
    protected State currentState = State.Default;
    protected Dictionary<State, IState> dicState = new Dictionary<State, IState>();

    protected Coroutine lifeTime = null;


    protected Color originColor;

    protected virtual void Awake()
    {
        slimeHitClip = Resources.Load<AudioClip>("Sounds/SlimeHit");
        originColor = MyRend.color;
    }

    protected virtual void Start()
    {
        //currHp = enemyData.maxHealth;
        move = GetComponent<AgentMove>();

        GameManager.Instance.onPlayerChangeType.AddListener(() => 
        {
            isShadow = !isShadow;
            MyRend.enabled = !isShadow;
            Anim.SetBool("isShadow", isShadow);
            gameObject.layer = 6;
        });
       
    }

    protected virtual void OnEnable()
    {
        isShadow = PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates);
        MyRend.enabled = !isShadow;
        currHP = enemyData.maxHealth;
        MyRend.color = originColor;
        isDie = false;
        lastAttackTime -= attackCool;
        EnemyManager.Instance.enemyList.Add(this);

        SetDefaultState(State.Default);
        lifeTime = StartCoroutine(LifeTime());
        //PoolManager.Instance.enemies.Add(this);
    }
    
    protected virtual void SetDefaultState(State state)     // 초기 행동 설정
    {
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void SetState(State state)
    {
        dicState[currentState].OnEnd();
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void PlayState(State state)
    {
        dicState[state].OnEnter();
    }

    private void Update()
    {
        if (Time.time - lastHitTime >= hitCool)
        {
            IsHit = false;
        }
    }

    protected virtual IEnumerator LifeTime()
    {
        yield return new WaitUntil(() => !IsDisarmed);

        float distance = (GameManager.Instance.player.position - transform.position).magnitude;

        if (PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates))
        {
            Shadow_Mode_Effect sme = PoolManager.Instance.Pop("Shadow Purse") as Shadow_Mode_Effect;
            sme.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }


        yield return new WaitForSeconds(.3f);

    }

    



    protected virtual void CheckHP()
    {
        if (currHP <= 0)
        {
            StopCoroutine(lifeTime);
            SetState(State.Die);
            //StageManager.Instance.curStageEnemys.Remove(this);
            isDie = true;
            StartCoroutine(Dead());
            OnDie?.Invoke();
            //SetDisable();
        }
        //isHit = false;
    }

    protected IEnumerator Blinking()
    {
        MyRend.color = color_Trans;
        yield return colorWait;
        MyRend.color = Color.white;
    }

    public virtual void GetHit(int damage)
    {
        if (isDie || isHit)
            return;

        isHit = true;
        lastHitTime = Time.time;
        float critical = Random.value;
        bool isCritical = false;
        if (critical <= GameManager.Instance.playerSO.attackStats.CTP)
        {
            damage *= 2; //2배 데미지
            isCritical = true;
        }

        if (currentState.Equals(State.Die)) return;

        SoundManager.Instance.GetAudioSource(slimeHitClip, false, SoundManager.Instance.BaseVolume).Play();
        currHP -= damage;

        StartCoroutine(Blinking());

        CheckHP();

        OnHit?.Invoke();

        DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        dPopup.gameObject.SetActive(true);
        dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0f), isCritical);

       
       

    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (isHit || isDie)
            return;
        move.KnockBack(direction, power, duration);
    }

    public virtual IEnumerator Dead()
    {
        if (isDie.Equals(true))
        {
            StageManager.Instance.ClearCheck();
            Anim.SetTrigger("isDie");
            yield return null;
        }
    }

    public void PushInPool()
    {
        PoolManager.Instance.Push(this);
    }


    public override void Reset()
    {
        OnReset?.Invoke();
        currHP = enemyData.maxHealth;
        Anim.ResetTrigger("isDie");
        Anim.Rebind();
        EnemyManager.Instance.enemyList.Remove(this);
        currentState = State.Default;
        isDie = false;
        isAttack = false;
        //myRend.enabled = true;

    }
}

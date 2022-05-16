using Pathfinding;
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

public enum EnemyState
{
    Default,    // 아무것도 없는 상태
    Move,       // 움직일 때
    Attack,     // 공격할 때
    Die         // 죽을 때
}

public class Enemy : PoolableMono, IAgent, IDamagable
{
    

    public EnemyDataSO enemyData;

    [field:SerializeField]
    protected float currHP = 0;
    public float CurrHP
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
                Move.rigid.velocity = Vector2.zero;
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
            //IsDisarmed = isHit;
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
    public SpriteRenderer MyRend
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

    private AgentMove move;
    public AgentMove Move
    {
        get 
        {
            if(move == null)
                move = GetComponent<AgentMove>();
            return move;
        } 
    }



    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }
    [field: SerializeField]
    public UnityEvent OnKockBack { get; set; }
    [field: SerializeField]
    public UnityEvent OnReset { get; set; }


    [HideInInspector]
    public AudioClip slimeHitClip;

    public bool isShadow = false;

    public float speed = 3f;

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);
    private Coroutine kockbackRoutine;

    [SerializeField]
    protected EnemyState currentState = EnemyState.Default;
    protected Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();

    protected Coroutine lifeTime = null;



    protected Color originColor;

    public AIDestinationSetter destinationSetter;
    public Seeker seeker;
    public AIPath path;

    public void SetAttack(bool value)
    {
        if (path != null && seeker != null && destinationSetter != null)
        {
            path.enabled = value;
            seeker.enabled = value;
            destinationSetter.enabled = value;
        }
    }

    protected virtual void Awake()
    {
        slimeHitClip = Resources.Load<AudioClip>("Sounds/SlimeHit");
        originColor = MyRend.color;
        OnKockBack = new UnityEvent();
        destinationSetter = GetComponentInParent<AIDestinationSetter>();
        seeker = GetComponentInParent<Seeker>();
        path = GetComponentInParent<AIPath>();
    }

    protected virtual void Start()
    {
        //currHp = enemyData.maxHealth;

        GameManager.Instance.onPlayerTypeChanged.AddListener(() => 
        {
            isShadow = PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates);
            //MyRend.enabled = !isShadow;
            Anim.SetBool("isShadow", isShadow);
            gameObject.layer = 6;
        });

        OnDie.AddListener(() =>
        {
            AddingEXP();
            GameManager.Instance.onPlayerGetEXP?.Invoke();
            SetAttack(false);
        });

        OnKockBack.AddListener(StartKockBack);

    }

    protected virtual void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            isShadow = PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates);
            MyRend.enabled = !isShadow;
        }
        currHP = enemyData.maxHealth;
        MyRend.color = originColor;
        isDie = false;
        lastAttackTime -= attackCool;
        EnemyManager.Instance.enemyList.Add(this);

        SetDefaultState(EnemyState.Default);
        lifeTime = StartCoroutine(LifeTime());
        //PoolManager.Instance.enemies.Add(this);
    }
    
    protected virtual void SetDefaultState(EnemyState state)     // 초기 행동 설정
    {
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void SetState(EnemyState state)
    {
        dicState[currentState].OnEnd();
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void PlayState(EnemyState state)
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

    private void AddingEXP()
    {
        GameManager.Instance.playerSO.ectStats.EXP += enemyData.exprencepoint;
    }

    protected virtual IEnumerator LifeTime()
    {
        yield return new WaitUntil(() => !IsDisarmed);

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
            SetState(EnemyState.Die);
            StageManager.Instance.curStageEnemys.Remove(this);
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

    public virtual void GetHit(float damage)
    {
        if (isDie || isHit)
            return;

        isHit = true;
        lastHitTime = Time.time;
        float critical = Random.value * 100;
        bool isCritical = false;
        if (critical <= GameManager.Instance.playerSO.attackStats.CTP)
        {
            float overPoint = 0;
            if (GameManager.Instance.playerSO.attackStats.CTP > 100)
            {
                overPoint = GameManager.Instance.playerSO.attackStats.CTP - 100;
            }

            Debug.Log(damage);

            damage *= (GameManager.Instance.playerSO.attackStats.CTD + overPoint) / 100;

            Debug.Log(damage);
            isCritical = true;
        }

        if (currentState.Equals(EnemyState.Die)) return;

        SoundManager.Instance.GetAudioSource(slimeHitClip, false, SoundManager.Instance.BaseVolume).Play();
        currHP -= damage;

        StartCoroutine(Blinking());

        CheckHP();

        OnHit?.Invoke();

        DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        dPopup.gameObject.SetActive(true);
        dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0f), isCritical);

       
       

    }

    public void StartKockBack()
    {
        if(kockbackRoutine == null)
            kockbackRoutine = StartCoroutine(SetKockBack());
    }

    IEnumerator SetKockBack()
    {
        SetAttack(false);
        yield return new WaitForSeconds(1f);
        SetAttack(true);

    }

    public virtual void KnockBack(Vector2 direction, float power, float duration)
    {
        if (isHit || isDie)
            return;
        OnKockBack?.Invoke();
        Move.KnockBack(direction, power, duration);
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

    public virtual void PushInPool()
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
        currentState = EnemyState.Default;
        isDie = false;
        isAttack = false;
        //myRend.enabled = true;

    }
}

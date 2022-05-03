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
    public UnityEvent OnReset { get; set; }


    [HideInInspector]
    public AudioClip slimeHitClip;

    public bool isShadow = false;

    public float speed = 3f;

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);

    [SerializeField]
    protected EnemyState currentState = EnemyState.Default;
    protected Dictionary<EnemyState, IState> dicState = new Dictionary<EnemyState, IState>();

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

        GameManager.Instance.onPlayerTypeChanged.AddListener(() => 
        {
            isShadow = PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates);
            MyRend.enabled = !isShadow;
            Anim.SetBool("isShadow", isShadow);
            gameObject.layer = 6;
        });
       
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
        float critical = Random.value;
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

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (isHit || isDie)
            return;
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
        currentState = EnemyState.Default;
        isDie = false;
        isAttack = false;
        //myRend.enabled = true;

    }
}

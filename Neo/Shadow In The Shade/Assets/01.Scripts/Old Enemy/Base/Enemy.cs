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

public enum OldEnemyState
{
    Default,    // 아무것도 없는 상태
    Move,       // 움직일 때
    Attack,     // 공격할 때
    Die         // 죽을 때
}

public class OldEnemy : PoolableMono, IAgent, IDamagable
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
    public int LastHitObjNumber { get; set; } = 0;

    [HideInInspector]
    public AudioClip slimeHitClip;

    public bool isShadow = false;

    public float speed = 3f;

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);
    private Coroutine kockbackRoutine;

    private bool isElite = false;

    private DamageEffect effect;

    [SerializeField]
    protected OldEnemyState currentState = OldEnemyState.Default;
    protected Dictionary<OldEnemyState, IState> dicState = new Dictionary<OldEnemyState, IState>();

    protected Coroutine lifeTime = null;



    protected Color originColor;

    public AIDestinationSetter destinationSetter;
    public Seeker seeker;
    public AIPath path;

    public void SetAttack(bool value)
    {
        if (path != null && seeker != null && destinationSetter != null)
        {
            if (!value)
                destinationSetter.target = null;
            path.enabled = value;
            seeker.enabled = value;
            destinationSetter.enabled = value;
            if (value)
                destinationSetter.target = GameManager.Instance.player;
        }
       
    }

    protected virtual void Awake()
    {
        slimeHitClip = Resources.Load<AudioClip>("Sounds/SlimeHit");
        originColor = MyRend.color;
        OnKockBack = new UnityEvent();
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
        path = GetComponent<AIPath>();
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
            //MyRend.enabled = !isShadow;
        }
        currHP = enemyData.maxHealth;
        MyRend.color = originColor;
        isDie = false;
        lastAttackTime -= attackCool;

        SetDefaultState(OldEnemyState.Default);
        lifeTime = StartCoroutine(LifeTime());
        //PoolManager.Instance.enemies.Add(this);
    }
    
    protected virtual void SetDefaultState(OldEnemyState state)     // 초기 행동 설정
    {
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void SetState(OldEnemyState state)
    {
        dicState[currentState].OnEnd();
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void PlayState(OldEnemyState state)
    {
        dicState[state].OnEnter();
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
            SetState(OldEnemyState.Die);
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

    public virtual void GetHit(float damage, int objNum)
    {
        if (objNum == LastHitObjNumber || isDie)
        {
            return;
        }
        LastHitObjNumber = objNum;
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

        if (currentState.Equals(OldEnemyState.Die)) return;


        effect = PoolManager.Instance.Pop("DamageEffect") as DamageEffect;
        effect.transform.position = transform.position;
        //effect.SetDamageEffect(transform.position, (GameManager.Instance.player.position - transform.position).normalized, isCritical);
        Invoke(nameof(PushDamageEffect), 1f);
        SoundManager.Instance.GetAudioSource(slimeHitClip, false, SoundManager.Instance.BaseVolume).Play();
        currHP -= damage;

        StartCoroutine(Blinking());

        CheckHP();

        OnHit?.Invoke();
        GameManager.Instance.onEnemyHit?.Invoke();

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
        kockbackRoutine = null;

    }

    public virtual void KnockBack(Vector2 direction, float power, float duration)
    {
        if ( isDie)
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
            PlayerSO so = GameManager.Instance.playerSO;
            if (so.attackStats.KAP != 0)
            {
                float kap = so.attackStats.KAP;
                so.attackStats.ATK += kap;
                yield return new WaitForSeconds(2f);
                so.attackStats.ATK -= kap;
            }
        }
    }

    public virtual void PushInPool()
    {
        PoolManager.Instance.Push(this);
    }

    private void PushDamageEffect()
    {
        if(effect != null)
        {
            PoolManager.Instance.Push(effect);
        }
    }

    public override void Reset()
    {
        OnReset?.Invoke();
        currHP = enemyData.maxHealth;
        Anim.ResetTrigger("isDie");
        Anim.Rebind();
        currentState = OldEnemyState.Default;
        isDie = false;
        isAttack = false;
        //myRend.enabled = true;

    }

    public void SetElite()
    {
        //체력 10배 공격력 1.5배
        CurrHP *= 10f;
        isElite = true;
        enemyData.damage *= 1.5f;
        transform.localScale *= 1.5f;
    }

    public void SetNomal()
    {
        if (isElite)
        {
            //체력 10배 공격력 1.5배
            CurrHP /= 10f;
            isElite = false;
            enemyData.damage /= 1.5f;
            transform.localScale /= 1.5f;
        }
    }
}

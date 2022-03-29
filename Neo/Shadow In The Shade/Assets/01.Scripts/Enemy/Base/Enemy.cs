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

    protected float currHp = 0f;

    public bool isAttack = false;
    public bool isDie = false;
    public bool isHit = false;

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

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);

    protected State currentState = State.Default;
    protected Dictionary<State, IState> dicState = new Dictionary<State, IState>();

    protected Coroutine lifeTime = null;

    private void Start()
    {
        //currHp = enemyData.maxHealth;
        move = GetComponent<AgentMove>();
        if (move == null)
            print("?");
    }

    protected void OnEnable()
    {
        currHp = enemyData.maxHealth;
        MyRend.color = Color.white;
        isDie = false;

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

    protected virtual IEnumerator LifeTime()
    {
        // 여기에 적의 로직 구현
        yield return null;
    }

    public virtual void GetDamage(float damage)
    {
        if (currentState.Equals(State.Die)) return;

        currHp -= damage;

        StartCoroutine(Blinking());

        CheckHp();
    }

    protected virtual void CheckHp()
    {
        if (currHp <= 0f)
        {
            print("사망 처리중");
            StopCoroutine(lifeTime);
            SetState(State.Die);
            isDie = true;
            StartCoroutine(Dead());
            OnDie?.Invoke();
            //SetDisable();
        }
        isHit = false;
    }

    protected IEnumerator Blinking()
    {
        MyRend.color = color_Trans;
        yield return colorWait;
        MyRend.color = Color.white;
    }

    public void SetHp(float hp)
    {
        currHp = hp;
    }

  
    public virtual void GetHit(int damage)
    {
        if (isDie || isHit)
            return;

        isHit = true;

        float critical = Random.value;
        bool isCritical = false;
        if (critical <= GameManager.Instance.playerSO.attackStats.CTP)
        {
            damage *= 2; //2배 데미지
            isCritical = true;
        }

        currHp -= damage;
        CheckHp();
        print($"{currHp}, {damage}");
        OnHit?.Invoke();

        DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        dPopup.gameObject.SetActive(true);
        dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0f), isCritical);
        print(dPopup.transform.position);

        //SoundManager.Instance.PlaySFX(SoundManager.Instance._slimeHitSFX);
        //if (currHp <= 0)
        //{
        //    isDie = true;
        //    StartCoroutine(Dead());
        //    //this.gameObject.SetActive(false);
        //    OnDie?.Invoke();
        //}

    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (isHit || isDie)
            return;
        if(move == null)
        {
            print("왜 없음?");
            return;
        }    
        move.KnockBack(direction, power, duration);
    }

   
    public virtual IEnumerator Dead()
    {
        if (isDie.Equals(true))
        {
            print("데드 코루틴");
            //_anim.SetBool("isDead", true);
            yield return new WaitForSeconds(.5f);
            this.gameObject.SetActive(false);
        }
    }
    public override void Reset()
    {
        OnReset?.Invoke();
        currHp = enemyData.maxHealth;
    }
}

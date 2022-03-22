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
        Default,    // �ƹ��͵� ���� ����
        Move,       // ������ ��
        Attack,     // ������ ��
        Die         // ���� ��
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
    public int Health { get; private set; }

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
        Health = enemyData.maxHealth;
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

    protected virtual void SetDefaultState(State state)     // �ʱ� �ൿ ����
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
        // ���⿡ ���� ���� ����
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
            SetState(State.Die);
            StopCoroutine(lifeTime);
            isDie = true;
            //SetDisable();
        }
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

    protected void OnTriggerEnter2D(Collider2D coll)
    {
        //Bullet bullet = coll.GetComponent<Bullet>();

        //if (bullet != null)
        //{
        //    GetDamage(bullet.Damage);
        //    bullet.SetDisable();
        //}
    }

   
    public void GetHit(int damage)
    {
        if (isDie || isHit) return;

        isHit = true;

        float critical = Random.value;
        bool isCritical = false;
        if (critical <= GameManager.Instance.playerSO.attackStats.CTP)
        {
            damage *= 2; //2�� ������
            isCritical = true;
        }

        Health -= damage;
        OnHit?.Invoke();

        DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        dPopup.gameObject.SetActive(true);
        dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0), isCritical);

        //SoundManager.Instance.PlaySFX(SoundManager.Instance._slimeHitSFX);
        if (Health <= 0)
        {
            isDie = true;
            StartCoroutine(Dead());
            //this.gameObject.SetActive(false);
            OnDie?.Invoke();
        }

        CheckHp();
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if(move == null)
        {
            print("�� ����?");
            return;
        }    
        move.KnockBack(direction, power, duration);
    }

   
    IEnumerator Dead()
    {
        if (isDie.Equals(true))
        {
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

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum EnemyBehaviorState
{
    Idle,
    Patrol,
    Chase,
    Attack
}

[System.Serializable]
public enum EnemyConditionState
{
    Normal,
    Slowed,
    Stunned,
    Attaced,
    Die
}



public class Enemy : PoolableMono, IAgent, IDamagable
{
    public EnemyBehaviorState enemyBehaviorState;
    public EnemyConditionState enemyConditionState;

    public UnityEvent OnDie { get; set; }
    public UnityEvent OnHit { get; set; }

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
            if (move == null)
                move = GetComponent<AgentMove>();
            return move;
        }
    }


    public int LastHitObjNumber { get; set; } = 0;

    public float maxHp;
    public float currentHp;
    public float experencePoint;

    public AIDestinationSetter destinationSetter;

    public Seeker seeker;

    public AIPath path;

    public bool isDie = false;


    private DamageEffect effect;


    protected Color originColor;

    

    public void Awake()
    {
        OnDie.AddListener(Dead);
        originColor = MyRend.color;
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
        path = GetComponent<AIPath>();
    }



    public void GetHit(float damage, int objNum)
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

        if (enemyConditionState.Equals(EnemyConditionState.Die)) return;


        effect = PoolManager.Instance.Pop("DamageEffect") as DamageEffect;
        effect.transform.position = transform.position;
        //effect.SetDamageEffect(transform.position, (GameManager.Instance.player.position - transform.position).normalized, isCritical);
        Invoke(nameof(PushDamageEffect), 1f);
        //SoundManager.Instance.GetAudioSource(slimeHitClip, false, SoundManager.Instance.BaseVolume).Play();
        currentHp -= damage;
        // StartCoroutine(Blinking());

        CheckHP();

        //OnHit?.Invoke();
        GameManager.Instance.onEnemyHit?.Invoke();

        DamagePopup dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopup;
        dPopup.gameObject.SetActive(true);
        dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0f), isCritical);


    }
    protected virtual void CheckHP()
    {
        if (currentHp <= 0)
        {
            SetConditionState(EnemyConditionState.Die);
            isDie = true;
            OnDie?.Invoke();
        }
    }

    private void Dead()
    {
        //여기에 적 죽는거 처리 해주면 됨
    }

    private void PushDamageEffect()
    {
        if (effect != null)
        {
            PoolManager.Instance.Push(effect);
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        //넉백 해주면 될듯
    }

    protected virtual void SetBehaviorState(EnemyBehaviorState state)
    {
        enemyBehaviorState = state;
    }

    protected virtual void SetConditionState(EnemyConditionState state)
    {
        enemyConditionState = state;
    }


    public override void Reset()
    {

    }
}

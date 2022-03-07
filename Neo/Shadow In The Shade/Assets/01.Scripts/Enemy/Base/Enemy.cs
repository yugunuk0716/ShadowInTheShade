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

public class Enemy : MonoBehaviour,IAgent,IDamagable
{
    protected enum State
    {
        Default,    // 아무것도 없는 상태
        Move,       // 움직일 때
        Attack,     // 공격할 때
        Die         // 죽을 때
    }

    public float maxHp = 3f;
    protected float currHp = 0f;
    public bool isDie = false;

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

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);

    protected State currentState = State.Default;
    protected Dictionary<State, IState> dicState = new Dictionary<State, IState>();

    protected Coroutine lifeTime = null;

    protected void OnEnable()
    {
        currHp = maxHp;
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
            SetState(State.Die);
            StopCoroutine(lifeTime);
            isDie = true;
            SetDisable();
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

    public virtual void SetDisable()
    {
        isDie = true;

        if (PoolManager.Instance != null)
        {
            //PoolManager.Instance.enemies.Remove(this);
        }

        gameObject.SetActive(false);
    }

    public void GetHit(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        throw new System.NotImplementedException();
    }

}

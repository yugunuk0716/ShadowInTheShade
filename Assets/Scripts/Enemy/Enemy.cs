using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent
{
    [SerializeField]
    private EnemySO _enemyData;

    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField]
    public EnemyAttack _enemyAttack { get; set; }
    private bool _isDead = false;

    //넉백 처리를 위한 에이전트 무브먼트 가져오기
    private EnemyMovement _enemyMovement;

    [field: SerializeField]
    public UnityEvent OnHit { get; set; }
    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnReset { get; set; }

    public Vector3 _hitPoint { get; private set; }
    public SpriteRenderer _sr;
    public Animator _anim;
    public Color _normalColor;
    public Color _shadowColor;

    private EnemyAI _enemyBrain;

    private void Awake()
    {
        if (_enemyAttack == null)
        {
            _enemyAttack = GetComponent<EnemyAttack>();
        }
        //에너미 브레인에 연결
        _enemyBrain = GetComponent<EnemyAI>();
        _enemyBrain.OnFireButtonPress += PerformAttack;
        _enemyMovement = GetComponent<EnemyMovement>();
        Health = _enemyData.MaxHealth;

        _sr = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        _anim.SetBool("isShadow", false);
        _normalColor = new Color(1, 1, 1, 1);

    }

    public void ShowShadowSprite()
    {
        bool isShadow = GameManager.Instance.currentPlayerSO.playerStates.Equals(PlayerStates.Shadow);
        _anim.SetBool("isShadow", isShadow);

        _sr.color = isShadow ? _shadowColor : _normalColor;
    }
    private void Start()
    {
        Health = _enemyData.MaxHealth;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;

        

        Health -= damage;
        _hitPoint = damageDealer.transform.position; 
        OnHit?.Invoke();

        //DamagePopUp dPopup = PoolManager.Instance.Pop("DamagePopup") as DamagePopUp;
        //dPopup?.Setup(damage, transform.position + new Vector3(0, 0.5f, 0));

        if (Health <= 0)
        {
            _isDead = true;
            OnDie?.Invoke();
        }
    }

   

    public void PerformAttack()
    {
        //사망하지 않음
        if (!_isDead)
        {
            _enemyAttack.Attack(_enemyData.Damage);
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        _enemyMovement.KnockBack(direction, power, duration);
    }

 

   
}

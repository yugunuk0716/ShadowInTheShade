using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent, IKnockBack
{
    [SerializeField]
    private EnemySO _enemyData;

    [field:SerializeField]
    public int Health { get;  private set; }

    private AgentMove _enemyMove;


    public SpriteRenderer _sr;
    public Animator _anim;

    public Color _shadowColor;
    private Color _normalColor;


    private bool _isDead = false;
    private bool _isHit = false;

    [field: SerializeField]
    public UnityEvent OnDie { get ; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get ; set; }


    


    private void Start()
    {
        Health = _enemyData.MaxHealth;
        _sr = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        _anim.SetBool("isShadow", false);
        _normalColor = new Color(1, 1, 1, 1);
        _enemyMove = GetComponent<AgentMove>();
        //GameManager.Instance.OnPlayerChangeType.AddListener(ShowShadowSprite);
    }

  

    public void ShowShadowSprite()
    {
        bool isShadow = GameManager.Instance.currentPlayerSO.playerStates.Equals(PlayerStates.Shadow);
        _anim.SetBool("isShadow", isShadow);

        _sr.color = isShadow ? _shadowColor : _normalColor;
        //_anim.gameObject.SetActive(!isShadow);
    }

    public void GetHit(int damage)
    {
        print(_isHit);
        if (_isDead || _isHit) return;

        _isHit = true;

        float critical = Random.value;
        bool isCritical = false;
        if (critical <= GameManager.Instance.currentPlayerSO.attackStats.CTP)
        {
            damage *= 2; //2배 데미지
            isCritical = true;
        }

        Health -= damage;
        OnHit?.Invoke();

        DamagePopup dPopup = PoolManager.Instance._damagePopupPool?.Allocate();
        dPopup.gameObject.SetActive(true);
        dPopup?.SetText(damage, transform.position + new Vector3(0, 0.5f, 0), isCritical);

        SoundManager.Instance.PlaySFX(SoundManager.Instance._slimeHitSFX);
        if (Health <= 0)
        {
            _isDead = true;
            this.gameObject.SetActive(false);
            OnDie?.Invoke();
        }

        Invoke(nameof(SetHit), _enemyData.HitDelay);
    }

   

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        _enemyMove.KnockBack(direction, power, duration);
        print("넉백 시작");
    }

    void SetHit()
    {
        _isHit = false;
    }

}

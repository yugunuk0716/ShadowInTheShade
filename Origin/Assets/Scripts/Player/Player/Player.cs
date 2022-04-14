using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IKnockBack, IHittable
{
    [SerializeField]
    private int _maxHealth;

    private int _health;

    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            PlayerHud.UpdateUI((float)_health, (float)_maxHealth);
        }
    }

    public float _hitDelay = 0.1f;

    [field: SerializeField]
    public PlayerHudUI PlayerHud { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }


    private bool _isDead = false;
    public bool _isHit = false;
    private AgentMove playerMove;



    void Start()
    {
        _maxHealth = GameManager.Instance.currentPlayerSO.ectStats.PHP;
        Health = _maxHealth;
        playerMove = GetComponent<AgentMove>();
        OnHit.AddListener(() =>
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance._playerHitSFX, 1f);
            EffectManager.Instance.BloodEffect(1f, 1f);
        });


    }

    


    public void GetHit(int damage)
    {
        if (_isDead || _isHit) return;

        _isHit = true;

        Health -= damage;
        OnHit?.Invoke();
        if (Health <= 0)
        {
            OnDie?.Invoke();
            _isDead = true;
        }

        Invoke(nameof(SetHit), _hitDelay);
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        playerMove.KnockBack(direction, power, duration);
    }

    void SetHit()
    {
        _isHit = false;
    }



}

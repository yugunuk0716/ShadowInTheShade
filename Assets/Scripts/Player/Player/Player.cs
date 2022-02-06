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

    [field: SerializeField]
    public PlayerHudUI PlayerHud { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get; set; }


    private bool _isDead = false;
    private AgentMove playerMove;



    void Start()
    {
        _maxHealth = GameManager.Instance.currentPlayerSO.ectStats.PHP;
        Health = _maxHealth;
        playerMove = GetComponent<AgentMove>();
    }

    


    public void GetHit(int damage)
    {
        if (_isDead) return;

        Health -= damage;
        OnHit?.Invoke();
        if (Health <= 0)
        {
            OnDie?.Invoke();
            _isDead = true;
        }
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        playerMove.KnockBack(direction, power, duration);
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent
{
    [SerializeField]
    private EnemySO _enemyData;

    [field:SerializeField]
    public int Health { get;  private set; }
    public SpriteRenderer _sr;
    public Animator _anim;

    private bool _isDead = false;

    [field: SerializeField]
    public UnityEvent OnDie { get ; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get ; set; }



    private void Start()
    {
        Health = _enemyData.MaxHealth;
        _sr = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        GameManager.Instance.OnPlayerChangeType.AddListener(ShowShadowSprite);
    }

    private void Update()
    {
        if(PlayerStates.Shadow == GameManager.Instance.currentPlayerSO.playerStates && Input.GetMouseButtonDown(0))
        {
            GetHit(1);
        }
    }

    private void ShowShadowSprite()
    {
        bool isShadow = GameManager.Instance.currentPlayerSO.playerStates == PlayerStates.Shadow;

        _anim.SetBool("isShadow", isShadow);
        _sr.sprite = isShadow ? _enemyData._shadowSprite : _enemyData._normalSprite;
    }

    public void GetHit(int damage)
    {
        if (_isDead) return;

        

        Health -= damage;
        OnHit?.Invoke(); 


        if (Health <= 0)
        {
            _isDead = true;
            this.gameObject.SetActive(false);
            OnDie?.Invoke();
        }
    }

   

    

}

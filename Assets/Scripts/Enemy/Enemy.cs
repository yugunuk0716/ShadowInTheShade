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
        _anim.SetBool("isShadow", false);
        //GameManager.Instance.OnPlayerChangeType.AddListener(ShowShadowSprite);
    }

  

    public void ShowShadowSprite()
    {
        bool isShadow = GameManager.Instance.currentPlayerSO.playerStates.Equals(PlayerStates.Shadow);
        //_anim.SetBool("isShadow", isShadow);
        _anim.gameObject.SetActive(!isShadow);
    }

    public void GetHit(int damage)
    {
        if (_isDead) return;

        

        Health -= damage;
        OnHit?.Invoke();

        SoundManager.Instance.PlaySFX(SoundManager.Instance._slimeHitSFX);
        if (Health <= 0)
        {
            _isDead = true;
            this.gameObject.SetActive(false);
            OnDie?.Invoke();
        }
    }

   

    

}

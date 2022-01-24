using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent
{
    [field:SerializeField]
    public int Health { get;  private set; }

    private bool _isDead = false;

    [field: SerializeField]
    public UnityEvent OnDie { get ; set; }
    [field: SerializeField]
    public UnityEvent OnHit { get ; set; }


    [field: SerializeField]
    public UnityEvent OnReset { get; set; }

    public Vector3 _hitPoint { get; private set; }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;

        

        Health -= damage;
        _hitPoint = damageDealer.transform.position; //피격한 녀석의 좌표 저장
        OnHit?.Invoke(); //구독중이라면 발행


        if (Health <= 0)
        {
            _isDead = true;
            OnDie?.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

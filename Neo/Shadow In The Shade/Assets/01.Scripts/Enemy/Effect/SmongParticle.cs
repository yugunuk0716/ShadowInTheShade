using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmongParticle : PoolableMono
{

    private ParticleSystemRenderer myRend;
    private CircleCollider2D coll;

    private void Awake()
    {
        myRend = GetComponent<ParticleSystemRenderer>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        GameManager.Instance.onPlayerChangeType.AddListener(() =>
        {
            myRend.enabled = !PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates);
        });
    }

    private void OnEnable()
    {
        coll.enabled = true;
    }

    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
        coll.enabled = false;
    }

    public override void Reset()
    {
        myRend.enabled = !PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates);
    }

    
   
}

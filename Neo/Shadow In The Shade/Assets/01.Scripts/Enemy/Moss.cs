using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss : DamagableObject
{
    public Animator _ainm;
    public bool _isAttacked;

    private void Awake()
    {
        _ainm = GetComponent<Animator>();
    }

    public void SetCreateAnimation()
    {
        _ainm.SetTrigger("created");
    }
    public void SetDeleteAnimation()
    {
        _ainm.SetTrigger("deleted");
    }
    public void SetFalse()
    {
        this.gameObject.SetActive(false);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, 1f, 0.7f);
        }
    }
}

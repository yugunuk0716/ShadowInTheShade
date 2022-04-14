using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss : PoolableMono
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
        Invoke(nameof(SetDelete), 2f);
    }

    public void SetDelete()
    {
        _ainm.SetTrigger("deleted");
    }


    public void SetFalse()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    base.OnTriggerEnter2D(collision);
    //    if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
    //    {
    //        IDamagable d = collision.GetComponent<IDamagable>();
    //        d.KnockBack(transform.position - collision.transform.position, 1f, 0.1f);
    //        EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, 1f, 0.7f);
    //    }
    //}
}

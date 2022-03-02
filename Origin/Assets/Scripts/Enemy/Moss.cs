using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moss : DamageObject
{
    public Animator _ainm;
    public bool _isAttacked;

    private void Start()
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
        gameObject.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isAttacked)
            return;
        base.OnTriggerEnter2D(collision);
    }
}

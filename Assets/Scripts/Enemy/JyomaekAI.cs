using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JyomaekAI : EnemyAI
{
    PlayerMove _playerMove;
    [Range(0f, 5f)]
    public float _slowAmount;
    public float _attachTime = 2f;
    public bool _isAttacked;

    private Vector3 _attachPostiion = new Vector3(0f, -0.2f, 0f);

    public override void Start()
    {
        base.Start();
        _playerMove = _target.GetComponent<PlayerMove>();
    }

    public override void AI()
    {
        if (_isAttacked)
        {
            transform.localPosition = _attachPostiion;
            return;
        }
        base.AI();
    }

    public override void Attack()
    {
        base.Attack();
        if (!_isAttacked)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        print("공격은 시작했음");
        _isAttacked = true;
        transform.SetParent(_target.transform);
        _playerMove._speed -= _slowAmount;

        yield return new WaitForSeconds(_attachTime);
        _playerMove._speed += _slowAmount;
        transform.SetParent(null);
        Vector3 dir = GameManager.Instance._dir * 1.2f;
        Vector3 randDir = new Vector3(Random.Range(1f, 1.5f), Random.Range(1f, 1.5f));
        if (Random.Range(0, 1) == 0)
            randDir *= -1f;
        transform.position = dir != Vector3.zero ? transform.position + -dir : transform.position + randDir;
        _isAttacked = false;
    }

}

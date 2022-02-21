using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossAI : EnemyAI
{
    public bool _isAttacked = false;
    public float _attackDelay;
    public GameObject mossPrefab;
    private IEnumerator _attack;

    public override void Start()
    {
        base.Start();
        _correction = -1;
    }

    public override void AI()
    {
        base.AI();
    }

    public override void Attack()
    {
        base.Attack();
        if (!_isAttacked)
        {
            _isAttacked = true;
            _attack = AttackCoroutine();
            StartCoroutine(_attack);
        }
       
    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            //Ǯ�Ŵ������� �̳� ����
            //�ӽ� �ν��Ͻÿ���Ʈ
            GameObject obj = Instantiate(mossPrefab);
            obj.transform.position = this.transform.position;
            yield return new WaitForSeconds(_attackDelay);
        }
    }


}

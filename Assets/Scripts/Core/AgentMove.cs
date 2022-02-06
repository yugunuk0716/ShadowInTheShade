using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMove :  MonoBehaviour , IMoveable
{

    public Rigidbody2D _rigid;

    public float _speed;


    protected bool _isKnockBack = false;

    protected Coroutine _knockBackCo = null;

    public void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

   
    public virtual void OnMove(Vector2 dir, float speed)
    {
        if (!_isKnockBack)
        {
            _rigid.velocity = new Vector2(dir.x * speed, dir.y * speed);
        }
        
    }

   

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (!_isKnockBack)
        {
            print("�˹� �߰�");
            _isKnockBack = true;
            _knockBackCo = StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }

  

    IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        print($"�˹� �ڷ�ƾ {_rigid.velocity}");
        _rigid.velocity = direction.normalized * power;
        yield return new WaitForSeconds(duration);
        ResetKnockBackParam();
    }

    private void ResetKnockBackParam()
    {
        print("�˹� �Ķ���� �ʱ�ȭ");
        _rigid.velocity = Vector2.zero;
        _isKnockBack = false;
    }
}

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
            _rigid.velocity = new Vector2(dir.x * speed * GameManager.Instance._timeScale, dir.y * speed * GameManager.Instance._timeScale);
        }
        
    }

   

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (!_isKnockBack)
        {
            _isKnockBack = true;
            _knockBackCo = StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }

  

    IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        _rigid.velocity = direction.normalized * power;
        yield return new WaitForSeconds(duration);
        ResetKnockBackParam();
    }

    private void ResetKnockBackParam()
    {
        _rigid.velocity = Vector2.zero;
        _isKnockBack = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMove : MonoBehaviour
{

    public Rigidbody2D rigid;

    protected bool _isKnockBack = false;

    protected Coroutine _knockBackCo = null;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    public void OnMove(Vector2 dir, float speed)
    {
        if (!_isKnockBack)
        {
            rigid.velocity = new Vector2(dir.x * speed, dir.y * speed);

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
        rigid.velocity = direction.normalized * power;
        yield return new WaitForSeconds(duration);
        ResetKnockBackParam();
    }

    private void ResetKnockBackParam()
    {
        rigid.velocity = Vector2.zero;
        _isKnockBack = false;
    }
}

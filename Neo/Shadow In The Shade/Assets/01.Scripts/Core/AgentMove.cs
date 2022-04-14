using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMove : MonoBehaviour
{

    public Rigidbody2D rigid;

    protected bool isKnockBack = false;

    protected Coroutine knockBackCo = null;

    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }


    public virtual void OnMove(Vector2 dir, float speed)
    {
        if (rigid == null)
            print($"{gameObject.name}에서 리짓바디가 안들어옴");
        if (!isKnockBack && rigid != null)
        {
            if (rigid == null)
                print("?");
            rigid.velocity = dir * speed;

        }
    }



    public virtual void KnockBack(Vector2 direction, float power, float duration)
    {
        if (!isKnockBack)
        {
            isKnockBack = true;
            knockBackCo = StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }

    protected IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        rigid.velocity = direction.normalized * power;
        yield return new WaitForSeconds(duration);
        ResetKnockBackParam();
    }

    protected void ResetKnockBackParam()
    {
        rigid.velocity = Vector2.zero;
        isKnockBack = false;
    }
}

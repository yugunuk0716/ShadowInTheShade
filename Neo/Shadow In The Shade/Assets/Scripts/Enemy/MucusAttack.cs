using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusAttack : EnemyAttack
{
    public float slowAmount = 3f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        rigid.velocity = Vector2.zero;
        base.Attack();

        if (!isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    public  IEnumerator AttackCoroutine()
    {
        print("점액 공격은 시작했음");
        isAttacking = true;
        transform.SetParent(target.transform);
        GameManager.Instance.playerSO.moveStats.SPD -= slowAmount;

        yield return new WaitForSeconds(attackDelay);
        GameManager.Instance.playerSO.moveStats.SPD += slowAmount;
        transform.SetParent(null);
        //Vector3 dir = GameManager.Instance._dir * 1.2f;
        Vector3 randDir = new Vector3(Random.Range(1f, 1.5f), Random.Range(1f, 1.5f));
        //if (Random.Range(0, 1) == 0)
        //    randDir *= -1f;
        transform.position = transform.position + randDir;
        isAttacking = false;
    }
}

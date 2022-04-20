using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Moss : MonoBehaviour, IState
{
    public float attackDelay = 5f;

    private bool isAttacking = false;

    public void OnEnter()
    {
        if (!isAttacking) 
        {
            StartCoroutine(AttackRoutine());
        }
    }

    public void OnEnd()
    {

    }


    IEnumerator AttackRoutine()
    {

        isAttacking = true;
        yield return new WaitForSeconds(attackDelay);

        Moss moss = PoolManager.Instance.Pop("Moss") as Moss;
        moss.transform.position = this.transform.position;
        isAttacking = false;

    }
}

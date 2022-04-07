using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Moss : MonoBehaviour, IState
{
    public float attackDelay = 5f;

    public void OnEnter()
    {
        StartCoroutine(AttackRoutine());
    }

    public void OnEnd()
    {

    }


    IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            //풀매니저에서 이끼 생성
            //임시 인스턴시에이트

            Moss moss = PoolManager.Instance.Pop("Moss") as Moss;
            moss.transform.position = this.transform.position;

            //GameObject obj = Instantiate(mossPrefab, this.transform.position, Quaternion.identity);
        }
    }
}

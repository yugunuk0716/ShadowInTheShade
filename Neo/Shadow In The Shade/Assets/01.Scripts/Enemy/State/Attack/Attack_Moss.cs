using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Moss : MonoBehaviour, IState
{
    public float attackDelay = 5f;
    private GameObject mossPrefab ;

    public void OnEnter()
    {
        if(mossPrefab == null)
            mossPrefab = Resources.Load<GameObject>("Moss");
        StartCoroutine(AttackRoutine());
    }

    public void OnEnd()
    {

    }


    IEnumerator AttackRoutine()
    {
        while (true)
        {
            //풀매니저에서 이끼 생성
            //임시 인스턴시에이트
            GameObject obj = Instantiate(mossPrefab);
            obj.transform.position = this.transform.position;
            yield return new WaitForSeconds(attackDelay);
        }
    }
}

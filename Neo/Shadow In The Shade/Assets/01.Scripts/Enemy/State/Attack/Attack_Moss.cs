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
            //Ǯ�Ŵ������� �̳� ����
            //�ӽ� �ν��Ͻÿ���Ʈ
            GameObject obj = Instantiate(mossPrefab);
            obj.transform.position = this.transform.position;
            yield return new WaitForSeconds(attackDelay);
        }
    }
}

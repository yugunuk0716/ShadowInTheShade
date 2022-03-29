using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Moss : MonoBehaviour, IState
{
    public bool isAttacked = false;
    public float attackDelay = 5f;
    private GameObject mossPrefab ;
    private IEnumerator attack;
    private bool isStateEnter = false;

    private Vector2 attachPosition = new Vector2(0f, -0.45f);

    public void OnEnter()
    {
        GameManager.Instance.onStateEnter?.Invoke();
        mossPrefab = Resources.Load<GameObject>("Moss");
        StartCoroutine(AttackRoutine());
        isStateEnter = true;
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

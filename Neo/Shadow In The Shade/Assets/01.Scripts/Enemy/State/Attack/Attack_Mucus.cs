using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Mucus : MonoBehaviour, IState
{
    public float slowAmount = 5f;
    public float attachTime = 2f;
    private bool isStateEnter = false;

    public void OnEnter()
    {
        GameManager.Instance.onStateEnter?.Invoke();
        StartCoroutine(AttackRoutine());
        isStateEnter = true;
    }

    public void OnEnd()
    {
       
    }

    private void Update()
    {
        if (!isStateEnter)
            return;
        transform.localPosition = Vector2.zero;
    }

    IEnumerator AttackRoutine()
    {
        transform.SetParent(GameManager.Instance.player);
        GameManager.Instance.playerSO.moveStats.SPD -= slowAmount;
        print(GameManager.Instance.playerSO.moveStats.SPD);
        yield return new WaitForSeconds(attachTime);
        GameManager.Instance.playerSO.moveStats.SPD += slowAmount;
        print(GameManager.Instance.playerSO.moveStats.SPD);
        transform.SetParent(null);
        Vector3 randDir = new Vector3(Random.Range(1f, 2f), Random.Range(1f, 2f));
        int idx = Random.Range(0, 2);
        if ( idx == 0)
        {
            randDir *= -1f;
        }
        transform.position = transform.position + randDir;
        GameManager.Instance.onStateEnd?.Invoke();
        isStateEnter = false;
    }


}

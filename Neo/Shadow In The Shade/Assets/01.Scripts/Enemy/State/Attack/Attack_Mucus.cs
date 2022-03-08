using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Mucus : MonoBehaviour, IState
{
    public float slowAmount = 3f;
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
        //if (GameManager.Instance.isInvincible)
        //{
        //    isStateEnter = false;
        //    GameManager.Instance.onStateEnd?.Invoke();
        //    yield break;
        //} 
        GameManager.Instance.isInvincible = true;
        transform.SetParent(GameManager.Instance.player);
        float spd = GameManager.Instance.playerSO.moveStats.SPD;
        GameManager.Instance.playerSO.moveStats.SPD = Mathf.Clamp(GameManager.Instance.playerSO.moveStats.SPD - slowAmount, 0, spd);
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

        //Invoke(nameof(InvincibleFalse), 1f);

    }


    //private void InvincibleFalse()
    //{
    //    GameManager.Instance.isInvincible = false;
    //}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Mushroom : MonoBehaviour, IState
{
    Transform target;

    Mushroom mushroom;

    bool canAttack = true;

    public void OnEnter()
    {
        if (!canAttack)
            return;

        canAttack = false;
        target = GameManager.Instance.player;
        mushroom = PoolManager.Instance.Pop("Mushroom") as Mushroom;
        mushroom.transform.position = this.transform.position;
        //GameObject obj = Instantiate(mushroomPrefab, this.transform.position, Quaternion.identity);
        Vector2 dir = target.position - this.gameObject.transform.position;
        print(dir.normalized * 2f);
        mushroom.Rigid.velocity = dir.normalized * 6f;
        StartCoroutine(CheckDistRoutine());

    }

    IEnumerator CheckDistRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if ((transform.position - mushroom.transform.position).sqrMagnitude > 25)
            {
                PoolManager.Instance.Push(mushroom);
                canAttack = true;
                yield break;
            }
        }
    }

    public void OnEnd()
    {
       
    }

}

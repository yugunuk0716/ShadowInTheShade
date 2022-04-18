using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move_Chase : MonoBehaviour, IState
{
    public float speed = 3f;

    public bool isStateEnter = false;
    public bool canTrace = false;

    private Coroutine chaseCoroutine;
    private Transform target;
    private AgentMove agentMove;

    private Enemy enemy;

    public void OnEnter()
    {
        if (isStateEnter)
            return;

        if (enemy == null)
            enemy = GetComponent<Enemy>();

        speed = enemy.speed;
        isStateEnter = true;

        if (target == null)
            target = GameManager.Instance.player;

        if(agentMove == null)
            agentMove = GetComponent<AgentMove>();

        if(chaseCoroutine != null) 
        {
            StopCoroutine(chaseCoroutine);
        }


        canTrace = true;
        chaseCoroutine = StartCoroutine(TrackingPlayer());
    }

    public void OnEnd()
    {
        if (chaseCoroutine != null)
        {
            print("Ã¼ÀÌ½º ¾Øµå");
            isStateEnter = false;
            canTrace = false;
            //StopCoroutine(chaseCoroutine);
            //agentMove.rigid.velocity = Vector3.zero;
        }
    }

    IEnumerator TrackingPlayer()
    {
        //while (true)
        //{
            if (target != null)
            {
            print(1);
                if (canTrace)
                {
                print(2);
                Vector2 dir = target.transform.position - this.gameObject.transform.position;

                    if (agentMove != null)
                    {
                    print(dir.normalized);
                        agentMove.OnMove(dir.normalized, speed);
                    }
                }
            }



            yield return null;
        //}
    }

    
}

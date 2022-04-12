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

        isStateEnter = true;

        if (target == null)
            target = GameManager.Instance.player;

        if(agentMove == null)
            agentMove = GetComponent<AgentMove>();

        if(chaseCoroutine != null) 
        {
            StopCoroutine(chaseCoroutine);
        }


        chaseCoroutine = StartCoroutine(TrackingPlayer());
        canTrace = true;
    }

    public void OnEnd()
    {
        if (chaseCoroutine != null)
        {
            isStateEnter = false;
            canTrace = false;
            StopCoroutine(chaseCoroutine);
        }
    }

    IEnumerator TrackingPlayer()
    {
        while (true)
        {
            if (target != null)
            {
                if (canTrace)
                {
                    Vector2 dir = target.transform.position - this.gameObject.transform.position;

                    if (agentMove != null)
                    {
                        agentMove.OnMove(dir.normalized, speed);
                    }
                }
            }



            yield return null;
        }
    }

    
}

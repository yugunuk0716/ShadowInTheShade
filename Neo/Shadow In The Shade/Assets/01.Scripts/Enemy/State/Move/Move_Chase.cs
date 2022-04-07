using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move_Chase : MonoBehaviour, IState
{
    public float speed = 3f;

    bool isStateEnter = false;
    bool canTrace = false;

    private Coroutine chaseCoroutine;
    private Transform target;
    private AgentMove agentMove;

    public void OnEnter()
    {
        if (isStateEnter)
            return;
        isStateEnter = true;
        if (target == null)
            target = GameManager.Instance.player;
        if(agentMove == null)
            agentMove = GetComponent<AgentMove>();
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
                    print("Ãß°Ý");
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

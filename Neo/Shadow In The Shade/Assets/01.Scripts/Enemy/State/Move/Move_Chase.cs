using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Move_Chase : MonoBehaviour, IState
{
    public float speed = 3f;

    private Coroutine chaseCoroutine;
    private Transform target;
    private AgentMove agentMove;

    public void OnEnter()
    {
        //transform.DOMove(GameManager.Instance.player.position, speed);
        target = GameManager.Instance.player;
        chaseCoroutine = StartCoroutine(TrackingPlayer());
        agentMove = GetComponent<AgentMove>();
        speed = 3f;
    }

    public void OnEnd()
    {
        if(chaseCoroutine != null)
        {
            speed = 0f;
            StopCoroutine(chaseCoroutine);
        }
    }

    IEnumerator TrackingPlayer()
    {
        while (true)
        {

            if (target != null)
            {
                Vector2 dir = target.transform.position - this.gameObject.transform.position;

                if (agentMove != null)
                {
                    agentMove.OnMove(dir.normalized , speed);
                }
            }



            yield return null;
        }
    }


    
}

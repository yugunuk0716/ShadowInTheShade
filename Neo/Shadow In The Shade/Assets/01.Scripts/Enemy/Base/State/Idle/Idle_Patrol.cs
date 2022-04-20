using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Patrol : MonoBehaviour, IState
{
    public float speed = 1.5f;

    private AgentMove agentMove;
    private Vector2 originPos = Vector2.zero;

    Coroutine moveRoutine;

    public void OnEnter()
    {
        if (agentMove == null)
            agentMove = GetComponent<AgentMove>();
        originPos = this.transform.position;
        moveRoutine = StartCoroutine(IdleCoroutine());
    }

    public void OnEnd()
    {
        if(moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
    }

    IEnumerator IdleCoroutine()
    {
        Vector2 moveDir = Vector2.zero;

        if (agentMove != null)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            if ((transform.position.x > originPos.x + 3f && x > 0) || transform.position.x < originPos.x - 3f && x < 0)
            {
                x *= -1;
            }
            else if ((transform.position.y > originPos.y + 3f && y > 0) || transform.position.y < originPos.y - 3f && y < 0)
            {
                y *= -1;
            }

            moveDir.Set(x, y);
            agentMove.OnMove(moveDir.normalized, speed);

            yield return new WaitForSeconds(.5f);
            moveDir = Vector2.zero;
            agentMove.OnMove(moveDir, speed);
        }
        yield return new WaitForSeconds(1f);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(originPos, 3);
            Gizmos.color = Color.white;
        }
    }
#endif

}

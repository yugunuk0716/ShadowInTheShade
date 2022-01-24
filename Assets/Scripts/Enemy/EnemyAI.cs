using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject _target;
    private AgentMove _agentMove;

    private void Start()
    {
        _target = GameManager.Instance.player.gameObject;
        _agentMove = transform.GetComponent<AgentMove>();
        StartCoroutine(TrackingTarget());
    }

    IEnumerator TrackingTarget()
    {
        while (true)
        {
            Vector2 dir = _target.transform.position - this.gameObject.transform.position;

            _agentMove.OnMove(dir.normalized, _agentMove.speed);

            yield return null;
        }
    }
}

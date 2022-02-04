using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject _target;

    public bool canMove = false;

    private IEnumerator _moveCoroutine;
    private AgentMove _agentMove;
    private Vector2 _originPos;

    private void Start()
    {
        _originPos = this.transform.position;
        _target = GameManager.Instance.player.gameObject;
        _agentMove = transform.GetComponent<AgentMove>();
        _agentMove.speed = canMove ? _agentMove.speed : 0f;
    }

    private void OnEnable()
    {
        _moveCoroutine = TrackingTarget();
        StartCoroutine(_moveCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(_moveCoroutine);
        transform.position = _originPos;
    }

    IEnumerator TrackingTarget()
    {
        while (true)
        {
            
            if (_target != null)
            { 
                Vector2 dir = _target.transform.position - this.gameObject.transform.position;

                if (_agentMove != null)
                {
                    _agentMove.OnMove(dir.normalized, _agentMove.speed);
                }
            }



            yield return null;
        }
    }
}

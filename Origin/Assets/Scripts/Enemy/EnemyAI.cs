using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject _target;

    public bool _canMove = false;
    [Range(0.1f, 15f)]
    public float _distance;
    [Range(0.1f, 15f)]
    public float _attackDistance;

    private IEnumerator _moveCoroutine;
    private AgentMove _agentMove;
    private Enemy _enemy;
    private Vector2 _originPos;
    public float _correction = 1;
    public float _speed;

    public virtual void Start()
    {
        _originPos = this.transform.position;
        _target = GameManager.Instance.player.gameObject;
        _agentMove = transform.GetComponent<AgentMove>();
        _enemy = GetComponent<Enemy>();
        _speed = _agentMove._speed;
    }

    public void Update()
    {
        AI();
    }

    public virtual void AI()
    {
        float dist = Vector2.Distance(_target.transform.position, this.transform.position);
        if (dist > _attackDistance && dist < _distance)
        {
            _speed = _agentMove._speed;
        }
        else if (_attackDistance > dist)
        {
            Attack();
        }
        else
        {
            _speed = 0;
        }
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

    public virtual void Attack()
    {
        //_enemy._anim.SetTrigger("attack");
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
                    _agentMove.OnMove(dir.normalized * _correction, _speed);
                }
            }



            yield return null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _attackDistance);
            Gizmos.color = Color.white;
        }
    }
#endif

}

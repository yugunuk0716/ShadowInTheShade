using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{

    protected AIActionData _aIActionData;
    protected EnemyMovementData _aIMovementData;
    [SerializeField]
    protected EnemyAI _enemyBrain;

    private void Awake()
    {
        _enemyBrain = transform.GetComponentInParent<EnemyAI>();
        _aIActionData = _enemyBrain.transform.GetComponentInChildren<AIActionData>();
        _aIMovementData = _enemyBrain.transform.GetComponentInChildren<EnemyMovementData>();

        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        //�ڽ� Awake���� ���ٰ� ������ ���⼭ ����
    }

    public abstract void TakeAction();
}

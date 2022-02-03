using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    protected AIActionData _aIActionData;
    protected EnemyMovementData _aIMovementData;
    protected EnemyAI _enemyAI;

    private void Awake()
    {
        _enemyAI = transform.GetComponentInParent<EnemyAI>();
        _aIActionData = _enemyAI.transform.GetComponentInChildren<AIActionData>();
        _aIMovementData = _enemyAI.transform.GetComponentInChildren<EnemyMovementData>();


        ChildAwake();
    }

    protected virtual void ChildAwake()
    {
        //자식 Awake에서 해줄게 있으면 여기서 구현
    }

    public abstract bool MakeADecision();
}

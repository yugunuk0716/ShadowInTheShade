using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{
    [field: SerializeField]
    public GameObject target { get; set; }

    [field: SerializeField]
    public AIState currentState { get; private set; }

    public Action<Vector2> OnMovementKeyPressed { get; set; }
    public Action<Vector2> OnPointerPositionChanged { get; set; }
    public Action OnFireButtonPress { get; set; }
    public Action OnFireButtonReleased { get; set; }

    private void Start()
    {
        target = GameManager.Instance.player.gameObject;
    }

    private void Update()
    {
        //타겟이 없으면 적군은 정지
        if (target == null)
        {
            OnMovementKeyPressed?.Invoke(Vector2.zero);
        }
        else
        {
            currentState.UpdateState(); //현재 상태를 업데이트
        }


    }

    public void Attack()
    {
        OnFireButtonPress?.Invoke();
    }
    public void Move(Vector2 movementDiraction, Vector2 targetPosition)
    {
        OnMovementKeyPressed?.Invoke(movementDiraction);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }
    public void ChangeToState(AIState nextState)
    {
        currentState = nextState;
    }
}

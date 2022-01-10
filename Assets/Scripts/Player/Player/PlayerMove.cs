using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMove : AgentMove
{
    private PlayerInput playerInput;

    private void Start()
    {
        GameManager.Instance.OnPlayerDash.AddListener(Dash);
        playerInput = GetComponent<PlayerInput>();
        speed = GameManager.Instance.currentPlayerSO.moveStats.SPD;
    }

    private void FixedUpdate()
    {
        OnMove(playerInput.dir.normalized, speed);
    }

    public override void OnMove(Vector2 dir, float speed)
    {
        base.OnMove(dir, speed);
    }

    public void Dash()
    {

    }
}

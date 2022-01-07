using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : AgentMove
{
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        //OnMove(playerInput.dir, speed);
    }

    public override void OnMove(Vector2 dir, float speed)
    {
        base.OnMove(dir, speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
   // [HideInInspector]
    public Vector2 dir;


    private void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("ChangePlayerType"))
        {
            ChangePlayerType();
        }

    }

    private void ChangePlayerType()
    {
        PlayerStates player = GameManager.Instance.currentPlayerSO.playerStates;

        if (player == PlayerStates.Human)
            player = PlayerStates.Shadow;
        else
            player = PlayerStates.Human;

        GameManager.Instance.currentPlayerSO.playerStates = player;
        GameManager.Instance.OnPlayerChangeType.Invoke();
    }
}

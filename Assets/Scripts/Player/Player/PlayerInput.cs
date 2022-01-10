using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
   // [HideInInspector]
    public Vector2 dir;


    private void Start()
    {
        GameManager.Instance.OnPlayerChangeType.AddListener(ChangePlayerType);
    }

    private void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("ChangePlayerType"))
        {
            if (GameManager.Instance.currentPlayerSO.canChangePlayerType)
            {
                GameManager.Instance.OnPlayerChangeType.Invoke();
                GameManager.Instance.currentPlayerSO.canChangePlayerType = false;
            }
            else
                print("준비되지 않았습니다");
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
    }
}

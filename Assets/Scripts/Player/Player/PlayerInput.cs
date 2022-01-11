using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
   // [HideInInspector]
    public Vector2 dir;

    private PlayerSO playerSO;

    private void Start()
    {
        GameManager.Instance.OnPlayerChangeType.AddListener(ChangePlayerType);
        playerSO = GameManager.Instance.currentPlayerSO;
    }

    private void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("ChangePlayerType"))
        {
            if (playerSO.canChangePlayerType)
            {
                GameManager.Instance.OnPlayerChangeType.Invoke();
                playerSO.canChangePlayerType = false;
            }
            else
                print("�غ���� �ʾҽ��ϴ�");
        }

    }

    private void ChangePlayerType()
    {
        PlayerStates ps = playerSO.playerStates;

        if (ps == PlayerStates.Human)
            ps = PlayerStates.Shadow;
        else
            ps = PlayerStates.Human;

        playerSO.playerStates = ps;

        GameManager.Instance.currentPlayerSO = playerSO;
    }
}
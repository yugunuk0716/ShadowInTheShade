using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 xMove;
    public bool isDash;
    public bool isAttack;
    public bool isUse;
    public bool isChangePlayerType; 


    private void Update()
    {

        if(GameManager.Instance.timeScale <= 0)
        {
            xMove = Vector2.zero;
            isDash = false;
            isAttack = false;
            isUse = false;
            return;
        }

        switch(GameManager.Instance.playerSO.playerStates) // �÷��̾� Ÿ�� ����
        {
            case PlayerStates.Human: // ��� ���� �϶� �����Ѱ� üũ
                switch (GameManager.Instance.playerSO.playerInputState) // �÷��̾� �Է� üũ
                {
                    case PlayerInputState.Dash:
                        isDash = Input.GetButtonDown("Dash"); 
                        break;
                    case PlayerInputState.Use:
                        isUse = Input.GetButtonDown("Use");
                        break;
                    case PlayerInputState.Move:
                        xMove.x = Input.GetAxisRaw("Horizontal");
                        xMove.y = Input.GetAxisRaw("Vertical");
                        break;
                }
                break;
            case PlayerStates.Shadow:// �׸��� ���� �϶� �����Ѱ� üũ
                switch (GameManager.Instance.playerSO.playerInputState) // �÷��̾� �Է� üũ
                {
                    case PlayerInputState.Attack:
                        xMove = Vector2.zero;
                        isAttack = Input.GetButtonDown("Fire1");
                        break;
                    case PlayerInputState.Move:
                        xMove.x = Input.GetAxisRaw("Horizontal");
                        xMove.y = Input.GetAxisRaw("Vertical");
                        break;
                }
                break;
        }

    }
}

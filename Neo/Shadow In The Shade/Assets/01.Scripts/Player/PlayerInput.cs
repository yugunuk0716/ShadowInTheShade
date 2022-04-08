using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveDir;
    public bool isDash;
    public bool isAttack;
    public bool isUse;
    public bool isChangePlayerType;
    public bool isHit;
    public bool isDie;


    private void Update()
    {
        if(GameManager.Instance.timeScale <= 0 || isDie)
        {
            moveDir = Vector2.zero;
            isDash = false;
            isAttack = false;
            isChangePlayerType = false;
            isUse = false;
            return;
        }

        switch(GameManager.Instance.playerSO.playerStates) // �÷��̾� Ÿ�� ����
        {
            case PlayerStates.Human: // ��� ���� �϶� �����Ѱ� üũ
                switch (GameManager.Instance.playerSO.playerInputState) // �÷��̾� �Է� üũ
                {
                    //case PlayerInputState.Hit:
                    //    isHit = true;
                    //    break;
                    case PlayerInputState.Dash:
                        isDash = Input.GetButtonDown("Fire1");
                        isChangePlayerType = Input.GetButtonDown("Change");
                        break;
                    case PlayerInputState.Use:
                        isUse = Input.GetButtonDown("Use");
                        break;
                    case PlayerInputState.Idle:
                    case PlayerInputState.Move:
                    case PlayerInputState.Change:
                        moveDir.x = Input.GetAxisRaw("Horizontal");
                        moveDir.y = Input.GetAxisRaw("Vertical");
                        isChangePlayerType = Input.GetButtonDown("Change");
                        isDash = Input.GetButtonDown("Fire1");
                        //isUse = Input.GetButtonDown("Use");
                        break;
                }
                break;
            case PlayerStates.Shadow:// �׸��� ���� �϶� �����Ѱ� üũ
                switch (GameManager.Instance.playerSO.playerInputState) // �÷��̾� �Է� üũ
                {
                    //case PlayerInputState.Hit:
                    //    isHit = true;
                    //    break;
                    case PlayerInputState.Attack:
                        moveDir = Vector2.zero;
                        isAttack = Input.GetButtonDown("Fire1");
                        break;
                    case PlayerInputState.Idle:
                    case PlayerInputState.Move:
                    case PlayerInputState.Change:
                        moveDir.x = Input.GetAxisRaw("Horizontal");
                        moveDir.y = Input.GetAxisRaw("Vertical");
                        isChangePlayerType = Input.GetButtonDown("Change");
                        isAttack = Input.GetButtonDown("Fire1");
                        break;
                }
                break;
        }

    }
}

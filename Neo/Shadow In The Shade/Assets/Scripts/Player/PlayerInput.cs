using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float xMove;
    public bool isDash;
    public bool isAttack;
    public bool isUse;


    private void Update()
    {

        if(GameManager.Instance.timeScale <= 0)
        {
            xMove = 0;
            isDash = false;
            isAttack = false;
            isUse = false;
            return;
        }

        switch(GameManager.Instance.playerSO.playerStates) // 플레이어 타입 상태
        {
            case PlayerStates.Human: // 사람 형태 일때 가능한걸 체크
                switch (GameManager.Instance.playerSO.playerInputState) // 플레이어 입력 체크
                {
                    case PlayerInputState.Dash:
                        isDash = Input.GetButtonDown("Dash"); 
                        break;
                    case PlayerInputState.Use:
                        isUse = Input.GetButtonDown("Use");
                        break;
                    case PlayerInputState.Move:
                        xMove = Input.GetAxisRaw("Horizontal");
                        break;
                }
                break;
            case PlayerStates.Shadow:// 그림자 형태 일때 가능한걸 체크
                switch (GameManager.Instance.playerSO.playerInputState) // 플레이어 입력 체크
                {
                    case PlayerInputState.Attack:
                        xMove = 0;
                        isAttack = Input.GetButtonDown("Fire1");
                        break;
                    case PlayerInputState.Move:
                        xMove = Input.GetAxisRaw("Horizontal");
                        break;
                }
                break;
        }

    }
}

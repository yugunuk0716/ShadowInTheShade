using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : AgentMove
{
    public PlayerInput playerInput;
    public Vector2 playerAxis;

    public void Start()
    {
        rigid = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        playerInput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }

    public void Update()
    {
        if (playerInput.isHit)
        {
            playerAxis = Vector2.zero;
            return;
        }

        if(!GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Dash))
        {
/*            if (GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Move) &&  //움직이고 있는데 상태가 Move가 아니면서 Attack일때도 아니고 Dashㄷ 아닐때
            !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack ) )
            {
                playerAxis = playerInput.moveDir.normalized;


                if (playerInput.moveDir == Vector2.zero)
                {
                    GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
                    playerAxis = Vector2.zero;
                }
            }
            else if (!GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack)) //움직이고 있는데 상태가 Attack이 아니고 Dash도 아닐때 무시
            {
                if (playerInput.moveDir != Vector2.zero)
                {
                    GameManager.Instance.playerSO.playerInputState = PlayerInputState.Move;
                    playerAxis = playerInput.moveDir.normalized;
                }
            }*/
            /*else if (GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))//움직이고 있는데 상태가 Attack일때 움직이는거 중지
            {
                playerAxis = Vector2.zero;
            }*/


            playerAxis = playerInput.moveDir.normalized;

            if (playerInput.moveDir == Vector2.zero)
            {
                GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
                playerAxis = Vector2.zero;
            }

            OnMove(playerAxis, GameManager.Instance.playerSO.moveStats.SPD);
        }
    }

    public override void OnMove(Vector2 dir, float speed)
    {
        base.OnMove(dir, speed);
    }
}

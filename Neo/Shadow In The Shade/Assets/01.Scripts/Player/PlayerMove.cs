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
/*            if (GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Move) &&  //�����̰� �ִµ� ���°� Move�� �ƴϸ鼭 Attack�϶��� �ƴϰ� Dash�� �ƴҶ�
            !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack ) )
            {
                playerAxis = playerInput.moveDir.normalized;


                if (playerInput.moveDir == Vector2.zero)
                {
                    GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
                    playerAxis = Vector2.zero;
                }
            }
            else if (!GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack)) //�����̰� �ִµ� ���°� Attack�� �ƴϰ� Dash�� �ƴҶ� ����
            {
                if (playerInput.moveDir != Vector2.zero)
                {
                    GameManager.Instance.playerSO.playerInputState = PlayerInputState.Move;
                    playerAxis = playerInput.moveDir.normalized;
                }
            }*/
            /*else if (GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))//�����̰� �ִµ� ���°� Attack�϶� �����̴°� ����
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

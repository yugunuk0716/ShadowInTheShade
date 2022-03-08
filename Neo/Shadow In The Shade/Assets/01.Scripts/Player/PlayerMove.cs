using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerinput;
    private Rigidbody2D rigd;
    public Vector2 playerAxis;

    public void Start()
    {
        rigd = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        playerinput = GameManager.Instance.player.GetComponent<PlayerInput>();
    }

    public void Update()
    {
        if(GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Move) && 
            !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))
        {
            playerAxis = playerinput.moveDir.normalized;


            if (playerinput.moveDir == Vector2.zero)
            {
                GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
                playerAxis = Vector2.zero;
            }
        }
        else if(!GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Attack))
        {
            if (playerinput.moveDir != Vector2.zero)
            {
                GameManager.Instance.playerSO.playerInputState = PlayerInputState.Move;
                playerAxis = playerinput.moveDir.normalized;
            }
        }
        else
        {
            playerAxis = Vector2.zero;
        }
       

        OnMove(playerAxis, GameManager.Instance.playerSO.moveStats.SPD);
    }

    public void OnMove(Vector2 dir,float speed)
    {
        rigd.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }
}

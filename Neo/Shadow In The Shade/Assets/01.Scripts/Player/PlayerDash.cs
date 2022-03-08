using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerInput playerinput;
    private Rigidbody2D rigd;
    private int dashStack;

    private void Start()
    {
        dashStack = 0;
        rigd = GetComponent<Rigidbody2D>();
        playerinput = GetComponent<PlayerInput>();
        StartCoroutine(StackPlus());
    }

    private void Update()
    {
        if (playerinput.isDash && !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Dash) && dashStack !=0)
        {
            print(playerinput.isDash);
            GameManager.Instance.playerSO.playerInputState = PlayerInputState.Dash;
            StartCoroutine(DashCoroutine());
            playerinput.isDash = false;
        }
    }

    IEnumerator StackPlus()
    {
        while(true)
        {
            dashStack++;
            yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DST);
        }
    }

    IEnumerator DashCoroutine()
    {
        if(dashStack <= 0)
        {
            yield break;
        }

        rigd.AddForce(playerinput.moveDir * GameManager.Instance.playerSO.moveStats.DPD, ForceMode2D.Impulse);
        dashStack--;
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DCT);
    }
}

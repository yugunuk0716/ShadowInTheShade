using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerInput playerinput;
    private Rigidbody2D rigid;

    private void Start()
    {
        GameManager.Instance.playerSO.moveStats.DSS = 0;
        rigid = GetComponent<Rigidbody2D>();
        playerinput = GetComponent<PlayerInput>();
        StartCoroutine(StackPlus());
    }

    private void Update()
    {
        if (playerinput.isDash && !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Dash) 
            && GameManager.Instance.playerSO.moveStats.DSS != 0)
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
            GameManager.Instance.playerSO.moveStats.DSS++;
            yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DST);
        }
    }

    IEnumerator DashCoroutine()
    {
        yield return null;
        if (GameManager.Instance.playerSO.moveStats.DSS <= 0 || playerinput.moveDir.normalized == Vector2.zero)
        {
            yield break;
        }
        rigid.AddForce(playerinput.moveDir.normalized * GameManager.Instance.playerSO.moveStats.DSP, ForceMode2D.Impulse);
        GameManager.Instance.playerSO.moveStats.DSS--;
        GameManager.Instance.onPlayerDash.Invoke();
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        rigid.velocity = Vector2.zero;
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        
    }
}

using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rigid;

    internal bool isDash;
    private float dashTime = 0.15f;
    private SpriteRenderer sr;

    private void Start()
    {
        GameManager.Instance.playerSO.moveStats.DSS = 0;
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(StackPlus());
    }

    private void Update()
    {

        if (playerInput.isDash && !GameManager.Instance.playerSO.playerInputState.Equals(PlayerInputState.Dash)
        && GameManager.Instance.playerSO.moveStats.DSS != 0 && playerInput.moveDir != Vector2.zero)
        {
            GameManager.Instance.playerSO.playerInputState = PlayerInputState.Dash;
            StartCoroutine(DashCoroutine());
            playerInput.isDash = false;
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameManager.Instance.playerSO.moveStats.DSS++;
        }
#endif
    }

    IEnumerator StackPlus()
    {
        while(true)
        {
            GameManager.Instance.playerSO.moveStats.DSS++;
            yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DST);
        }
    }

    internal IEnumerator DashCoroutine()
    {
        isDash = true;
        yield return null;
        if (GameManager.Instance.playerSO.moveStats.DSS <= 0 || playerInput.moveDir.normalized == Vector2.zero)
        {
            yield break;
        }
        rigid.AddForce(playerInput.moveDir.normalized * GameManager.Instance.playerSO.moveStats.DSP, ForceMode2D.Impulse);
        float time = 0;
        float afterTime = 0;
        float targetTime = Random.Range(0.02f, 0.06f);


        while (isDash)
        {
            time += Time.deltaTime;
            afterTime += Time.deltaTime;

            if (afterTime >= targetTime)
            {
                AfterImage ai = PoolManager.Instance.Pop("AfterImage") as AfterImage;
                if(ai != null && sr != null)
                {
                    ai.SetSprite(sr.sprite, transform.position);
                }
                else
                {
                    print("ºö");
                }
                targetTime = Random.Range(0.02f, 0.06f);
                afterTime = 0;
            }
            

            if (time >= dashTime)
            {
                isDash = false;
            }
            yield return null;
        }
        GameManager.Instance.playerSO.moveStats.DSS--;
        GameManager.Instance.onPlayerDash.Invoke();
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        rigid.velocity = Vector2.zero;
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        
    }
}

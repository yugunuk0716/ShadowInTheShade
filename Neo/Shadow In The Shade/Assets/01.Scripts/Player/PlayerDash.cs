using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rigid;

    internal bool isDash;
    private float dashTime = 0.15f;
    private SpriteRenderer sr;

    private AudioClip dashAudioClip;


    private int originLayer;
    private readonly int targetLayer = 9;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(StackPlus());
        dashAudioClip = Resources.Load<AudioClip>("Sounds/PlayerDash");
        originLayer = gameObject.layer;
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
            yield return new WaitUntil(() => GameManager.Instance.playerSO.moveStats.DSS < GameManager.Instance.playerSO.moveStats.MDS);
            yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DST);
            GameManager.Instance.playerSO.moveStats.DSS++;


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

        Sequence seq = DOTween.Sequence();

        if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow))
        {
            gameObject.layer = targetLayer;

            seq.Append(sr.DOFade(0f, .1f));
            //seq.Insert(.5f, sr.DOFade(1f, .1f));
            TimeManager.Instance.ModifyTimeScale(0f, .5f, () => { sr.DOFade(1f, .1f); TimeManager.Instance.ModifyTimeScale(1f, .5f); });
        
        }

        
        rigid.AddForce(playerInput.moveDir.normalized * GameManager.Instance.playerSO.moveStats.DSP, ForceMode2D.Impulse);

        if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
        {
            SoundManager.Instance.GetAudioSource(dashAudioClip, false, SoundManager.Instance.BaseVolume).Play();
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
                    if (ai != null && sr != null)
                    {
                        ai.SetSprite(sr.sprite, transform.position);
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
        }
        else
        {
            float time2 = 0;
            while (isDash)
            {
                time2 += Time.deltaTime;
                if (time2 >= dashTime)
                {
                    
                    isDash = false;
                   
                }
                yield return null;
            }
          
        }

       

        //if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow))
        //{
        //    yield return new WaitForSeconds(3f);
        //    //isDash = false;
        //}

        GameManager.Instance.playerSO.moveStats.DSS--;
        GameManager.Instance.onPlayerDash.Invoke();
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        rigid.velocity = Vector2.zero;
        gameObject.layer = originLayer;
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        
    }
}

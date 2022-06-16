using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerNewDash : MonoBehaviour
{
    float spd =0;
    public float timeSlowSpeed;
    public ParticleSystem chargeEffect;
    public bool isCharging;
    private PlayerInput playerInput;
    private Rigidbody2D rigd;
    private float effectRunTime;
    private Vector2 lateDir;
    private PlayerAnimation playerAnimation;
    private PlayerDashCollider dashCollider;
    private Camera cam;
    private Vector3 mousePos;
    private IEnumerator co;

    internal bool isDash;
    internal bool isTypeChanged;
    private SpriteRenderer sr;
    private float dashTime = 0.15f;

    
    public static bool usedDash = false;
    


    public void Start()
    {
        // lateDir = Vector2.zero;
        co = DashAttackSpeedIncrese();
        rigd = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        chargeEffect.gameObject.SetActive(false);
        chargeEffect.Stop();
        chargeEffect.gameObject.transform.position = transform.position;
        isCharging = false;
        isTypeChanged = false;
        effectRunTime = 0f;
        spd = GameManager.Instance.playerSO.moveStats.SPD;
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        dashCollider = GetComponentInChildren<PlayerDashCollider>();
        //cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        sr = GetComponentInChildren<SpriteRenderer>();

        GameManager.Instance.onPlayerChangeType.AddListener(() =>
        {
            if (isCharging)
            {
                isTypeChanged = true;
               
            }
        });

    }



    public void Update()
    {
       

        if (Input.GetButton("Dash"))
        {

            if (usedDash)
                return;

            if (isTypeChanged)
            {
                chargeEffect.gameObject.SetActive(false);
                chargeEffect.GetComponent<ParticleSystem>().Stop();
                effectRunTime = 0f;
                ResetCharging();
                GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
                gameObject.layer = 3;
                GameManager.Instance.playerSO.moveStats.SPD = 7f;
                dashCollider.isDashing = false;
                GameManager.Instance.playerSO.playerDashState = PlayerDashState.Default;
                rigd.velocity = Vector2.zero;
                return;
            }

            GameManager.Instance.playerSO.moveStats.SPD = 
                Mathf.Clamp(GameManager.Instance.playerSO.moveStats.SPD -= Time.deltaTime * timeSlowSpeed, 1f, 7f);
            
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isCharging == false)
            {
                isCharging = true;
                chargeEffect.gameObject.SetActive(true);
                chargeEffect.GetComponent<ParticleSystem>().Play();
            }

            if(isCharging == true)
            {
                effectRunTime += Time.deltaTime;
            }

        }

        if (Input.GetButtonUp("Dash"))
        {

            if (isTypeChanged)
            {
                isTypeChanged = false;
            }


            chargeEffect.gameObject.SetActive(false);
            chargeEffect.GetComponent<ParticleSystem>().Stop();
            //GameManager.Instance.playerSO.moveStats.SPD = 0f;
            if (isCharging)
            {
                if(effectRunTime >= .6f)
                {
                    usedDash = true;
                    StartCoroutine(Dashing(2.5f));
                    GameManager.Instance.playerSO.playerDashState = PlayerDashState.Power3;
                    if(GameManager.Instance.playerSO.attackStats.BSP != 0)
                    {
                        StopCoroutine(co);
                        StartCoroutine(co);
                    }
                    Debug.Log("DashMax");
                }
                else if(effectRunTime > .4f)
                {
                    usedDash = true;
                    StartCoroutine(Dashing(1.5f));
                    GameManager.Instance.playerSO.playerDashState = PlayerDashState.Power2;
                    Debug.Log("DashHalf");
                }
                else if(effectRunTime > .2f)
                {
                    usedDash = true;
                    StartCoroutine(Dashing(1f));
                    GameManager.Instance.playerSO.playerDashState = PlayerDashState.Power1;
                    Debug.Log("DashMin");
                }
                else
                {
                    Invoke("ResetCharging", .2f);
                    GameManager.Instance.playerSO.playerDashState = PlayerDashState.Default;
                    rigd.velocity = Vector2.zero;
                    return;
                }


               // Debug.Log("Dash");
            }
            else
            {
                Invoke("ResetCharging", .2f);
                GameManager.Instance.playerSO.playerDashState = PlayerDashState.Default;
                rigd.velocity = Vector2.zero;
            }

            DOTween.To(() => GameManager.Instance.playerSO.moveStats.SPD, x => GameManager.Instance.playerSO.moveStats.SPD = x, 7, .1f);
            effectRunTime = 0f;

            // transform.DOMove(, .4f);
            // Debug.Log((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).no);
        }
    }

    public IEnumerator DashAttackSpeedIncrese()
    {
        GameManager.Instance.playerSO.attackStats.ASD += GameManager.Instance.playerSO.attackStats.BSP;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.playerSO.attackStats.ASD = GameManager.Instance.playerSO.attackStats.BSP;
    }


    public void ResetCharging()
    {
        isCharging = false;
    }
   
    public IEnumerator Dashing(float dashPower)
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Dash;
        dashCollider.isDashing = true;
        GameManager.Instance.onPlayerDash?.Invoke();

        if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
        {
          //  SoundManager.Instance.GetAudioSource(dashAudioClip, false, SoundManager.Instance.BaseVolume).Play();
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

        //Debug.Log(playerInput.moveDir.normalized * GameManager.Instance.playerSO.moveStats.DSP);
        gameObject.layer = 9;
        // rigd.AddForce(lateDir * GameManager.Instance.playerSO.moveStats.DSP * dashPower, ForceMode2D.Impulse);

        //Debug.Log(mousePos.normalized * GameManager.Instance.playerSO.moveStats.DSP * dashPower);
        float ast = 2f;

        float dist = Vector3.Distance(mousePos, transform.position);


        if (dist > 15)
        {
            ast = 1.5f;
        }
        else if(dist > 13.3f)
        {
            ast = 2.5f;
        }
        else if (dist > 13.2f)
        {
            ast = 4f;
        }
        else if (dist > 13.1f)
        {
            ast = 6f;
        }
        else if (dist > 13.05f)
        {
            ast = 7f;
        }
        else if(dist > 13f)
        {
            ast = 10f;
        }


        GameManager.Instance.onPlayerDash?.Invoke();

        rigd.AddForce((mousePos - transform.position).normalized *  GameManager.Instance.playerSO.moveStats.DSP * dashPower * ast, ForceMode2D.Impulse);
        playerAnimation.CallShadowDashAnime(dashPower >= 2.5 ? 2 : dashPower >= 1.5f ? 1 : 0);


        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        rigd.velocity = Vector2.zero;
        ResetCharging();
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        gameObject.layer = 3;
        GameManager.Instance.playerSO.moveStats.SPD = 7f;
        dashCollider.isDashing = false;
        GameManager.Instance.playerSO.playerDashState = PlayerDashState.Default;

        gameObject.layer = 7;
        yield return new WaitForSeconds(.3f);
        gameObject.layer = 3;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
//        if(collision.gameObject.layer.Equals(6) && 
//            GameManager.Instance.playerSO.playerInputState == PlayerInputState.Dash)
//        {
//            Debug.Log("pl");
//            for (int i = 0; i < 5; i++)
//            {
//                GameObject e;
//                e = PoolManager.Instance.Pop("ShadowEffect").gameObject;
///*                e.SetActive(true);
//                e.GetComponent<BezierObj>().origin = collision.gameObject;*/
//            }

//            StartCoroutine(CallonHumanDashCrossEnemy(collision));
//        }
    }

  
}
#region ÁÖ¼®
//RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(transform.position, lateDir, GameManager.Instance.playerSO.moveStats.DSP, LayerMask.GetMask("Enemy"));
//float x = 1f;
//float y = 1f;

//if(rigd.velocity.x > 0)
//{
//    x = 5f;
//}
//if (rigd.velocity.x < 0)
//{
//    x = -5f;
//}
//if (rigd.velocity.y > 0)
//{
//    y = 5f;
//}
//if (rigd.velocity.y < 0)
//{
//    y = -5f;
//}

//if(x < 0 && y < 0)
//{

//}

//Collider2D[] coll2Ds = Physics2D.OverlapBoxAll(transform.position + (Vector3)rigd.velocity.normalized, new Vector2(x,y), 0f, LayerMask.GetMask("Enemy"));
////foreach (RaycastHit2D hit2D in hit2Ds)
//foreach (Collider2D coll2D in coll2Ds)
//{
//    if (GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Human))
//    {
//        print(coll2D.gameObject.layer);
//        //if (coll2D.gameObject.layer == 6)
//        {
//            for (int i = 0; i < 5; i++)
//            {
//                print(coll2D.name);
//                GameObject e = PoolManager.Instance.Pop("ShadowEffect").gameObject;
//                e.transform.position = coll2D.transform.position;
//            }
//            StartCoroutine(CallonHumanDashCrossEnemy(coll2D));
//        }
//    }
//}

#endregion
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

    public void Start()
    {
        lateDir = Vector2.zero;
        rigd = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        chargeEffect.gameObject.SetActive(false);
        chargeEffect.Stop();
        chargeEffect.gameObject.transform.position = transform.position;
        isCharging = false;
        effectRunTime = 0f;
        spd = GameManager.Instance.playerSO.moveStats.SPD;
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
    }


    public void Update()
    {
        if(playerInput.moveDir.normalized != Vector2.zero)
        {
            lateDir = playerInput.moveDir.normalized;
        }

        if (Input.GetButton("Dash"))
        {
            GameManager.Instance.playerSO.moveStats.SPD = 
                Mathf.Clamp(GameManager.Instance.playerSO.moveStats.SPD -= Time.deltaTime * timeSlowSpeed, 1f, 7f);


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

            chargeEffect.gameObject.SetActive(false);
            chargeEffect.GetComponent<ParticleSystem>().Stop();
            GameManager.Instance.playerSO.playerInputState = PlayerInputState.Dash;

            //GameManager.Instance.playerSO.moveStats.SPD = 0f;
            if (isCharging)
            {
                if(effectRunTime >= 1.1f)
                {
                    StartCoroutine(Dashing(2.5f));
                    Debug.Log("DashMax");
                }
                else if(effectRunTime > .7f)
                {
                    StartCoroutine(Dashing(1.5f));
                    Debug.Log("DashHalf");
                }
                else if(effectRunTime > .2f)
                {
                    StartCoroutine(Dashing(1f));
                    Debug.Log("DashMin");
                }
                else
                {
                    Invoke("ResetCharging", .2f);
                    return;
                }


               // Debug.Log("Dash");
            }
            else
            {
                Invoke("ResetCharging", .2f);
            }

            DOTween.To(() => GameManager.Instance.playerSO.moveStats.SPD, x => GameManager.Instance.playerSO.moveStats.SPD = x, 7, .1f);
            effectRunTime = 0f;

            // transform.DOMove(, .4f);
            // Debug.Log((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).no);
        }
    }

    public void ResetCharging()
    {
        isCharging = false;
    }

    public IEnumerator Dashing(float dashPower)
    {
        //Debug.Log(playerInput.moveDir.normalized * GameManager.Instance.playerSO.moveStats.DSP);
       // gameObject.layer = 9;
        rigd.AddForce(lateDir * GameManager.Instance.playerSO.moveStats.DSP * dashPower, ForceMode2D.Impulse);
        playerAnimation.CallShadowDashAnime(dashPower >= 2.5 ? 2 : dashPower >= 1.5f ? 1 : 0);
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        rigd.velocity = Vector2.zero;
        ResetCharging();
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        //gameObject.layer = 3;
        GameManager.Instance.playerSO.moveStats.SPD = 7f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer.Equals(6) && 
            GameManager.Instance.playerSO.playerInputState == PlayerInputState.Dash)
        {
            Debug.Log("pl");
            for (int i = 0; i < 5; i++)
            {
                GameObject e;
                e = PoolManager.Instance.Pop("ShadowEffect").gameObject;
/*                e.SetActive(true);
                e.GetComponent<BezierObj>().origin = collision.gameObject;*/
            }

            StartCoroutine(CallonHumanDashCrossEnemy(collision));
        }
    }

    public IEnumerator CallonHumanDashCrossEnemy(Collider2D collision)
    {
        yield return null;
        GameManager.Instance.onHumanDashCrossEnemy.Invoke(collision.gameObject);

    }
}

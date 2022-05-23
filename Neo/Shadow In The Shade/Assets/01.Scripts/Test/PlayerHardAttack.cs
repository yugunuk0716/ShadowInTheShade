using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHardAttack : MonoBehaviour
{
    float spd =0;
    public float timeSlowSpeed;
    public ParticleSystem chargeEffect;
    public bool isCharging;
    private PlayerInput playerInput;
    private Rigidbody2D rigd;

    public void Start()
    {
        rigd = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        chargeEffect.gameObject.SetActive(false);
        chargeEffect.Stop();
        chargeEffect.gameObject.transform.position = transform.position;
        isCharging = false;
        spd = GameManager.Instance.playerSO.moveStats.SPD;
    }


    public void Update()
    {
        if(Input.GetMouseButton(0))
        {
            GameManager.Instance.playerSO.moveStats.SPD = 
                Mathf.Clamp(GameManager.Instance.playerSO.moveStats.SPD -= Time.deltaTime * timeSlowSpeed, 1f, 7f);


            if (isCharging == false)
            {
                isCharging = true;
                chargeEffect.gameObject.SetActive(true);
                chargeEffect.GetComponent<ParticleSystem>().Play();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //GameManager.Instance.playerSO.moveStats.SPD = 0f;
            if (isCharging && GameManager.Instance.playerSO.moveStats.SPD <= 3f)
            {
                GameManager.Instance.playerSO.playerInputState = PlayerInputState.Dash;
                Debug.Log("Dash");
                StartCoroutine(Dashing());
            }
            else
            {
                Invoke("ResetCharging", .2f);
            }

            DOTween.To(() => GameManager.Instance.playerSO.moveStats.SPD, x => GameManager.Instance.playerSO.moveStats.SPD = x, 7, .1f);
            // transform.DOMove(, .4f);
            // Debug.Log((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).no);
        }
    }

    public void ResetCharging()
    {
        isCharging = false;
    }

    public IEnumerator Dashing()
    {
        Debug.Log(playerInput.moveDir.normalized * GameManager.Instance.playerSO.moveStats.DSP);    
        rigd.AddForce(playerInput.moveDir.normalized * GameManager.Instance.playerSO.moveStats.DSP * 2,ForceMode2D.Impulse);
        yield return new WaitForSeconds(GameManager.Instance.playerSO.moveStats.DRT);
        rigd.velocity = Vector2.zero;
        ResetCharging();
        GameManager.Instance.playerSO.playerInputState = PlayerInputState.Idle;
        GameManager.Instance.playerSO.moveStats.SPD = 7f;
    }
}

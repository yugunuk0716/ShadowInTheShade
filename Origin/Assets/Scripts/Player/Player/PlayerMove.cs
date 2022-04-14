using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerMove : AgentMove
{
    private PlayerInput playerInput;
    private Player player;
    private SpriteRenderer _sr;
    private bool isDash = false;
    private Vector2 mousePos;
    private Vector2 playerMousePos;
    private float dashAngle;
    private float dashTime = 0.15f;
    private float dashPower = 12f;

    private void Start()
    {
        GameManager.Instance.OnPlayerDash.AddListener(() => 
        {
            StartCoroutine(DashCoroutine());
            SoundManager.Instance.PlaySFX(SoundManager.Instance._playerDashSFX, 0.6f);
        });
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
        _sr = GetComponent<SpriteRenderer>();
        _speed = GameManager.Instance.currentPlayerSO.moveStats.SPD;
    
    }

    private void FixedUpdate()
    {
        if (player._isHit)
            return;

        if (GameManager.Instance.isAttack)
        {
            OnMove(transform.position, 0);
            return;
        }

        if (!isDash)
            OnMove(playerInput.dir.normalized, _speed);
    }

    public override void OnMove(Vector2 dir, float speed)
    {
        base.OnMove(dir, speed);
    }

    IEnumerator DashCoroutine()
    {
        StopNormalMoving();
        print($"{playerInput.dir * dashPower}");
        _rigid.AddForce(playerInput.dir * dashPower, ForceMode2D.Impulse);

        float time = 0;
        float afterTime = 0;
        float targetTime = Random.Range(0.02f, 0.06f);
        while (isDash)
        {/*
            print("?????? ????");*/
            time += Time.deltaTime;
            afterTime += Time.deltaTime;

            print($"{afterTime} {targetTime}");

            if (afterTime >= targetTime)
            {
                AfterImage ai = PoolManager.Instance.GetAfterImage();
                ai.SetSprite(_sr.sprite, transform.position);
                targetTime = Random.Range(0.02f, 0.06f);
                afterTime = 0;
            }

            if (time >= dashTime)
            {
                isDash = false;
            }
            yield return null;
        }
        _rigid.velocity = Vector2.zero;
        StartCoroutine(CheckDashEnd());
    }

    //public void Dash()
    //{
    //    StopNormalMoving();
    // /*   mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    playerMousePos = (mousePos - (Vector2)transform.position).normalized;
    //    dashAngle = Vector2.Angle((Vector2)transform.position + Vector2.up, playerMousePos);
    //    Debug.Log(dashAngle);*/
    //    OnMove(playerInput.dir.normalized, GameManager.Instance.currentPlayerSO.moveStats.DPD);
    //    StartCoroutine(CheckDashEnd());
    //}

    public void StopNormalMoving()
    {
        isDash = true;
    }

    public void RestartNormalMoving()
    {
        isDash = false;
    }

    public IEnumerator CheckDashEnd()
    {
        yield return new WaitForSeconds(GameManager.Instance.currentPlayerSO.moveStats.DRT);
        RestartNormalMoving();        
    }
}

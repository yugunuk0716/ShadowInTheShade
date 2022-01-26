using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerMove : AgentMove
{
    private PlayerInput playerInput;
    private bool isDashing = false;
    private Vector2 mousePos;
    private Vector2 playerMousePos;
    private float dashAngle;

    private void Start()
    {
        GameManager.Instance.OnPlayerDash.AddListener(Dash);
        playerInput = GetComponent<PlayerInput>();
        speed = GameManager.Instance.currentPlayerSO.moveStats.SPD;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isAttack)
        {
            OnMove(transform.position, 0);
            return;
        }

        if (!isDashing)
            OnMove(playerInput.dir.normalized, speed);
    }

    public override void OnMove(Vector2 dir, float speed)
    {
        base.OnMove(dir, speed);
    }
    public void Dash()
    {
        StopNormalMoving();
     /*   mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerMousePos = (mousePos - (Vector2)transform.position).normalized;
        dashAngle = Vector2.Angle((Vector2)transform.position + Vector2.up, playerMousePos);
        Debug.Log(dashAngle);*/
        OnMove(playerInput.dir.normalized, GameManager.Instance.currentPlayerSO.moveStats.DPD);
        StartCoroutine(CheckDashEnd());
    }

    public void StopNormalMoving()
    {
        isDashing = true;
    }

    public void RestartNormalMoving()
    {
        isDashing = false;
    }

    public IEnumerator CheckDashEnd()
    {
        float time = GameManager.Instance.currentPlayerSO.moveStats.DRT;
        while (true)
        {
            if(time <= 0)
            {
                RestartNormalMoving();
                yield break;
            }
            else
            {
                time -= Time.deltaTime;
                yield return null;
            }
        }
        
    }
}

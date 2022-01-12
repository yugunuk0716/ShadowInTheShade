using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class PlayerMove : AgentMove
{
    private PlayerInput playerInput;
    private bool isDashing = false;

    private void Start()
    {
        GameManager.Instance.OnPlayerDash.AddListener(Dash);
        playerInput = GetComponent<PlayerInput>();
        speed = GameManager.Instance.currentPlayerSO.moveStats.SPD;
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            OnMove(playerInput.dir.normalized, speed);
    }

    private void Update()
    {
       // LookMouse();
    }

    public override void OnMove(Vector2 dir, float speed)
    {
        base.OnMove(dir, speed);
    }
    public void Dash()
    {
        StopNormalMoving();
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mouse - transform.position).normalized;
        dir = new Vector2(Mathf.Clamp(dir.x,-1f,1f), Mathf.Clamp(dir.y, -1f, 1f));
        OnMove(dir, GameManager.Instance.currentPlayerSO.moveStats.DPD);
        StartCoroutine(CheckDashEnd());
    }

    public void LookMouse()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation
                                                            ,Quaternion.Euler(0,0,angle)
                                                            ,Time.deltaTime * GameManager.Instance.currentPlayerSO.ectStats.LPS);
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

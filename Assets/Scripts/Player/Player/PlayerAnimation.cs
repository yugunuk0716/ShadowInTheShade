using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerAnimation : MonoBehaviour
{
    private SpriteRenderer sr;
    private PlayerSO playerSO;
    private PlayerInput pi;
    private Animator anim;

    private Vector2 moveVec;
    private Vector2 moveLastVec;

    public void Start()
    {
        anim = GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();
        playerSO = GameManager.Instance.currentPlayerSO;
        GameManager.Instance.OnPlayerChangeType.AddListener(ChangePlayerTypeAnimation);
    }

    private void Update()
    {
        UpdatePlayerAnimation();
    }

    private void UpdatePlayerAnimation()
    {
        if ((pi.dir.x == 0 && pi.dir.y == 0) && moveVec.x != 0 || moveVec.y != 0)
        {
            moveLastVec = moveVec;
            anim.SetFloat("AnimLastMoveX", moveLastVec.x);
            anim.SetFloat("AnimLastMoveY", moveLastVec.y);
        }

        moveVec = pi.dir.normalized;
        anim.SetFloat("AnimMoveX", moveVec.x);
        anim.SetFloat("AnimMoveY", moveVec.y);
        anim.SetFloat("AnimMoveMagnitude", moveVec.magnitude);
    }

    public void ChangePlayerTypeAnimation()
    {
        Color targetColor;

        if (playerSO.playerStates.Equals(PlayerStates.Human))
            targetColor = Color.black;
        else
            targetColor = Color.white;

        sr.DOColor(targetColor, playerSO.ectStats.TCT).OnComplete(() => 
        {
            playerSO.canChangePlayerType = true;
            GameManager.Instance.currentPlayerSO = playerSO;
        });
    }
}

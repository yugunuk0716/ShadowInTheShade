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
    private int attackCount =0;

    public void Start()
    {
        anim = GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();
        playerSO = GameManager.Instance.currentPlayerSO;
        GameManager.Instance.OnPlayerChangeType.AddListener(() => StartCoroutine(ChangePlayerTypeAnimation()));
        GameManager.Instance.OnPlayerAttack.AddListener(() => StartCoroutine(PlayerAttack()));
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

    public IEnumerator ChangePlayerTypeAnimation()
    {
        yield return null;
        if (playerSO.playerStates.Equals(PlayerStates.Human))
            anim.SetBool("IsShadow", false);
        else
            anim.SetBool("IsShadow", true);


        yield return new WaitForSeconds(playerSO.ectStats.TCT);
        playerSO.canChangePlayerType = true;
        GameManager.Instance.currentPlayerSO = playerSO;


        /*
                sr.DOColor(targetColor, playerSO.ectStats.TCT).OnComplete(() => 
                {
                    playerSO.canChangePlayerType = true;
                    GameManager.Instance.currentPlayerSO = playerSO;
                });*/
    }

    public IEnumerator PlayerAttack()
    {
        if (attackCount <= 0)
            attackCount++;
        else
            attackCount = 0;

        anim.SetBool("IsAttack",true);  
        anim.SetInteger("AttackCount", attackCount);
        yield return new WaitForSeconds(.5f);
        anim.SetBool("IsAttack", false);
    }

    public bool GetBool(string propName)
    {
        return anim.GetBool(propName);
    }
}

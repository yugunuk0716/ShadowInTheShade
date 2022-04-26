using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerWeapon : DamagableObject
{
    RaycastHit2D hit2D;
    public PlayerAnimation playerAnim;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.DrawRay(transform.position, playerAnim.lastMoveDir, Color.white, 1f);

        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            hit2D = Physics2D.Raycast(transform.position, playerAnim.lastMoveDir, 1f, LayerMask.GetMask("Wall"));
            if (hit2D.collider == null)
            {

                GameManager.Instance.feedBackPlayer.PlayFeedback();

                base.OnTriggerEnter2D(collision);
            }
            
        }

    }




}

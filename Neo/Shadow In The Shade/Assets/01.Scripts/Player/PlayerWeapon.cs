using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerWeapon : DamagableObject
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            GameManager.Instance.feedBackPlayer.PlayFeedback();
        }
        //Time.timeScale = 0.8;
        base.OnTriggerEnter2D(collision);
    }


   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerWeapon : DamagableObject
{
    RaycastHit2D hit2D;
    public PlayerAnimation playerAnim;

    public void Start()
    {
        dObjData.damage = 0;
        dObjData.damage = GameManager.Instance.playerSO.attackStats.ATK;
        dObjData.hitNum = 1;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        dObjData.damage = GameManager.Instance.playerSO.attackStats.ATK;

        Debug.DrawRay(transform.position, playerAnim.lastMoveDir, Color.white, 1f);

        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            hit2D = Physics2D.Raycast(transform.position, playerAnim.lastMoveDir, 1f, LayerMask.GetMask("Wall"));
            if (hit2D.collider == null)
            {

                GameManager.Instance.feedBackPlayer.PlayFeedback();
                if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
                {
                    IDamagable damagable = collision.GetComponent<IDamagable>();
                    damagable?.KnockBack((collision.transform.position - GameManager.Instance.player.position).normalized, dObjData.knockBackPower, dObjData.knockBackDelay);

                    print(dObjData.hitNum);
                    damagable?.GetHit(dObjData.damage, dObjData.hitNum);
                }
            }
            
        }

    }




}

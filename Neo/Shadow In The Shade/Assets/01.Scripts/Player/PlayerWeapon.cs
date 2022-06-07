using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerWeapon : DamagableObject
{
    RaycastHit2D hit2D;
    public PlayerAnimation playerAnim;
    private bool isAttacked = false;

    public void Start()
    {
        dObjData.damage = 0;
        dObjData.damage = GameManager.Instance.playerSO.attackStats.ATK;
        dObjData.hitNum = 100;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (isAttacked)
            return;

        dObjData.damage = GameManager.Instance.playerSO.attackStats.ATK;

        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            hit2D = Physics2D.Raycast(transform.position, playerAnim.lastMoveDir, 1f, LayerMask.GetMask("Wall"));
            if (hit2D.collider == null)
            {
                isAttacked = true;
                //GameManager.Instance.feedBackPlayer.PlayFeedback();
                IDamagable damagable = collision.GetComponent<IDamagable>();
                Vector2 vec = (collision.transform.position - GameManager.Instance.player.position).normalized;

                if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
                {
                    vec.Set(vec.x, 0);
                }
                else
                {
                    vec.Set(0, vec.y);
                }

                damagable?.KnockBack(vec, dObjData.knockBackPower, dObjData.knockBackDelay);
                damagable?.GetHit(dObjData.damage, dObjData.hitNum);
                GameManager.Instance.onPlayerAttackSuccess.Invoke();

                isAttacked = false;
            }

        }

    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        dObjData.damage = GameManager.Instance.playerSO.attackStats.ATK;

        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            hit2D = Physics2D.Raycast(transform.position, playerAnim.lastMoveDir, 1f, LayerMask.GetMask("Wall"));
            if (hit2D.collider == null)
            {

                GameManager.Instance.feedBackPlayer.PlayFeedback();
                if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
                {
                    IDamagable damagable = collision.GetComponent<IDamagable>();
                    Vector2 vec = (collision.transform.position - GameManager.Instance.player.position).normalized;

                    if(Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
                    {
                        vec.Set(vec.x, 0);
                    }
                    else
                    {
                        vec.Set(0, vec.y);
                    }

                    damagable?.KnockBack(vec, dObjData.knockBackPower, dObjData.knockBackDelay);
                    dObjData.hitNum++;
                    damagable?.GetHit(dObjData.damage, dObjData.hitNum);
                    GameManager.Instance.onPlayerAttackSuccess.Invoke();
                }
            }
            
        }

    }




}

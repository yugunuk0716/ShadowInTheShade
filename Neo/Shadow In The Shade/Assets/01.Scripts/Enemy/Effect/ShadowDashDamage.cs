using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDashDamage : DamagableObject
{
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if(!GameManager.Instance.playerSO.playerDashState.Equals(PlayerDashState.Default) && GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow))
        {
            if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
            {
                float damagePerType = 0.5f;


                if(GameManager.Instance.playerSO.playerDashState.Equals( PlayerDashState.Power3))
                {
                    damagePerType = 1f;
                }

                IDamagable damagable = collision.GetComponent<IDamagable>();
                dObjData.hitNum += 3;
                damagable?.KnockBack((collision.transform.position - transform.position).normalized, dObjData.knockBackPower, dObjData.knockBackDelay);
                damagable?.GetHit(dObjData.damage * damagePerType, dObjData.hitNum);
            }
        }
    }
}

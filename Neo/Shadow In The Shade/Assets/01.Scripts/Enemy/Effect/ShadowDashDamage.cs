using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDashDamage : DamagableObject
{
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if(!GameManager.Instance.playerSO.playerDashState.Equals(PlayerDashState.Default) && GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow))
        {
            print(GameManager.Instance.playerSO.playerStates);
            if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
            {
                float damagePerType = 0.5f;


                if(GameManager.Instance.playerSO.playerDashState.Equals( PlayerDashState.Power3))
                {
                    damagePerType = 1f;
                }

                IDamagable damagable = collision.GetComponent<IDamagable>();
                damagable?.KnockBack((collision.transform.position - this.transform.position).normalized, dObjData.knockBackPower, dObjData.knockBackDelay);
                damagable?.GetHit(dObjData.damage * damagePerType );
            }
        }
    }
}

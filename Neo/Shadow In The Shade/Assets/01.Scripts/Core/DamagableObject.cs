using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour
{
    public LayerMask whatIsTarget;


    public DamagableObjectSO dObjData;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            IDamagable damagable = collision.GetComponent<IDamagable>();
            damagable?.KnockBack((collision.transform.position - transform.position).normalized, dObjData.knockBackPower, dObjData.knockBackDelay);
            damagable?.GetHit(dObjData.damage, dObjData.hitNum);
        }
    }


}

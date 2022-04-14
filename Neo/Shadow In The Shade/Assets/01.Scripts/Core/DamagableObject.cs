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
            Time.timeScale = 0.9f;
            IDamagable damagable = collision.GetComponent<IDamagable>();

            damagable?.KnockBack((collision.transform.position - this.transform.position).normalized, dObjData.knockBackPower, dObjData.knockBackDelay);
            damagable?.GetHit(dObjData.damage);
            Invoke(nameof(SetTimeScale), 0.5f);
        }
    }

    void SetTimeScale()
    {
        Time.timeScale = 1f;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEnemy : DamagableObject
{
    public Enemy enemy;
    public Enemy Enemy
    {
        get
        {
            if (enemy == null)
                enemy = GetComponentInParent<Enemy>();
            return enemy;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy.isAttack)
        {

            if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
            {
                IDamagable d = collision.GetComponent<IDamagable>();
                if (d.IsHit)
                    return;
                base.OnTriggerEnter2D(collision);
                EffectManager.Instance.BloodEffect(EffectType.SLIME, 0.5f, 1f, 0.7f);
            }
        }
    }
}

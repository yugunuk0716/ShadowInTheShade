using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEnemy : DamagableObject
{
    RaycastHit2D hit2D;

    private Enemy enemy;
    public Enemy Enemy
    {
        get
        {
            if (enemy == null)
            {
                enemy = GetComponentInParent<Enemy>();
                
            }
            return enemy;
        }
    }

    private void OnEnable()
    {
        dObjData.damage = Enemy.enemyData.damage;
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (Enemy.isAttack)
            {
                if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
                {
                    hit2D = Physics2D.Raycast(transform.position, transform.position - collision.transform.position, 2f, LayerMask.GetMask("Wall"));
                    if (hit2D.collider == null)
                    {
                        IDamagable d = collision.GetComponent<IDamagable>();
                        if (d.IsHit)
                            return;
                        base.OnTriggerEnter2D(collision);
                    }


            }
        }
        else
        {
            if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
            {
                hit2D = Physics2D.Raycast(transform.position, transform.position - collision.transform.position, 2f, LayerMask.GetMask("Wall"));
                if (hit2D.collider == null)
                {
                    IDamagable d = collision.GetComponent<IDamagable>();
                    if (d.IsHit)
                        return;

                    d?.KnockBack((collision.transform.position - this.transform.position).normalized, dObjData.knockBackPower / 2, dObjData.knockBackDelay);
                    d?.GetHit(dObjData.damage / 2);
                }
                //base.OnTriggerEnter2D(collision);
            }
        }

    }
}

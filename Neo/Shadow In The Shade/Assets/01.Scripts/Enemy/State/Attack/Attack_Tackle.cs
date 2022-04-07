using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Tackle : DamagableObject, IState
{
    Enemy enemy;
    ITacklable tacklable;


    public void OnEnter()
    {
        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }

        if (tacklable == null)
        {
            tacklable = GetComponentInParent<ITacklable>();
        }

        if (enemy != null)
        {
            tacklable.SetTackle(true);
            Vector3 vec = (GameManager.Instance.player.position - transform.position).normalized;
            enemy.Anim.SetFloat("MoveX", vec.x); // Mathf.Clamp(vec.x, -1f, 1f));
            enemy.Anim.SetFloat("MoveY", vec.y); //Mathf.Clamp(vec.y, -1f, 1f));
            enemy.move.OnMove(vec, 10f);
            enemy.Anim.SetBool("isTackle", true);
        }
    }

    public void OnEnd()

    {
    }

    public void TackleEnd() 
    {
        if (enemy != null)
        {
            Invoke(nameof(AttackReset), 1);
            enemy.Anim.SetBool("isTackle", false);
        }
    }

    void AttackReset()
    {
        tacklable.SetTackle(false);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (enemy != null)
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




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public LayerMask whatIsTarget;

    public DamageObjectSO damageObjectSO;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (((1 << collision.gameObject.layer & whatIsTarget) > 0))// && GameManager.Instance.isAttack)
        {
            print($"{collision.gameObject.name}");
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();
            IKnockBack kb = collision.GetComponent<IKnockBack>();

            print($"{(collision.transform.position - this.transform.position).normalized}");
            kb?.KnockBack((collision.transform.position - this.transform.position).normalized, damageObjectSO._knockBackPower, damageObjectSO._knockBackDelay);
            hittable?.GetHit(damageObjectSO._damage);

            
        }
    }

   
}

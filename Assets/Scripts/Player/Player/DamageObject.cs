using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public LayerMask whatIsTarget;

    PlayerAnimation anim;
    int damage = 1;
    

    private void Start()
    {
        anim = GetComponentInParent<PlayerAnimation>();
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer & whatIsTarget) > 0 ) && GameManager.Instance.isAttack)
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();

            //hittable?.GetHit(damage);
        }
    }
}

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
        GameManager.Instance.OnPlayerAttack.AddListener(() => {
            if (anim.GetBool("IsAttack"))
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance._playerAttackSFX);
            } 
        } );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();

            hittable?.GetHit(damage);
        }
    }
}

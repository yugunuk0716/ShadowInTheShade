using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerAnimation anim;
    int damage = 1;
    

    private void Start()
    {
        anim = GetComponent<PlayerAnimation>();
        GameManager.Instance.OnPlayerAttack.AddListener(() => {
            if (anim.GetBool("IsAttack"))
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance._playerAttackSFX);
            } 
        } );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && anim.GetBool("IsAttack"))
        {
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();

            hittable?.GetHit(damage);
        }
    }
}

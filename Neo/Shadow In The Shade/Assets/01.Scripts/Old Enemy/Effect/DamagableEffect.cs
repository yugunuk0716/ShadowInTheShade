using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableEffect : DamagableObject
{
    RaycastHit2D hit2D;


    protected override void OnTriggerEnter2D(Collider2D collision)
    {


        if ((1 << collision.gameObject.layer & whatIsTarget) > 0)
        {
            Debug.DrawRay(transform.position, transform.position - collision.transform.position, Color.white, 2f);
            hit2D = Physics2D.Raycast(transform.position, transform.position - collision.transform.position, 2f, LayerMask.GetMask("Wall"));
            if (hit2D.collider == null)
            {
                base.OnTriggerEnter2D(collision);
            }

        }

    }
}

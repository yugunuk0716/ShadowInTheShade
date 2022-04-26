using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : PoolableMono
{
    private Rigidbody2D rigid;
    public Rigidbody2D Rigid
    {
        get
        {
            if (rigid == null)
                rigid = GetComponent<Rigidbody2D>();
            return rigid;
        }
    }

    private Animator anim;

    public bool isPushed = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
       
        GameManager.Instance.onPlayerChangeType.AddListener(() =>
        {
            anim.SetBool("isShadow", PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
            

        });
    }



    private void OnEnable()
    {
        anim.SetBool("isShadow", PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
        isPushed = false;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print(collision.gameObject.tag);
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        isPushed = true;
    //        this.rigid.velocity = Vector2.zero;
    //        PoolManager.Instance.Push(this);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isPushed = true;
            this.rigid.velocity = Vector2.zero;
            PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {

    }

}

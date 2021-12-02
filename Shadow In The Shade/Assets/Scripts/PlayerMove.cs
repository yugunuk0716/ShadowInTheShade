using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 7f;



    [Header("대시 관련")]
    public GameObject afterImagePrefab;
    public Transform afterImageTrm;
    public bool canDash = false;// 대쉬아이템 먹었는지 여부
    public float dashPower = 10f;
    public float dashTime = 0.2f;
    public float dashCooltime = 5f;

    private Rigidbody2D rigid;
    private PlayerInput input;
    private SpriteRenderer spriteRenderer;

    private float curDashCooltime;
    private bool isDash = false;
    private bool isHit = false;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.isDash && !isDash && canDash && curDashCooltime <= 0)
        {
            isDash = true;
            curDashCooltime = dashCooltime;
            StartCoroutine(Dash());
        }

        if (curDashCooltime > 0) 
        {
            curDashCooltime -= Time.deltaTime;
            if (curDashCooltime <= 0) curDashCooltime = 0;
        }

        if (input.isDash) 
        {
            print("switch");
        }
    }

    IEnumerator Dash()
    {
        Vector2 dir = spriteRenderer.flipX ? transform.right * -1 : transform.right;
        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir * dashPower, ForceMode2D.Impulse);
        rigid.gravityScale = 0;

        float time = 0;
        float afterTime = 0;
        float targetTime = Random.Range(0.02f, 0.06f);
        while (isDash)
        {
            time += Time.deltaTime;
            afterTime += Time.deltaTime;

            //if (afterTime >= targetTime)
            //{
            //    AfterImage ai = PoolManager.GetItem<AfterImage>();
            //    ai.SetSprite(spriteRenderer.sprite, spriteRenderer.flipX, transform.position);
            //    targetTime = Random.Range(0.02f, 0.06f);
            //    afterTime = 0;
            //}

            if (time >= dashTime)
            {
                isDash = false;
            }
            yield return null;
        }
        rigid.velocity = Vector2.zero;
        rigid.gravityScale = 1;
    }

}

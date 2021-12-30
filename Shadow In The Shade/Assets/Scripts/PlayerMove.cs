using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float _moveSpeed = 7f;



    [Header("대시 관련")]
    public GameObject _afterImagePrefab;
    public Transform _afterImageTrm;
    public float _dashPower = 10f;
    public float _dashTime = 0.2f;
    public float _dashCooltime = 5f;

    private Rigidbody2D _rigid;
    private PlayerInput _input;
    private SpriteRenderer _spriteRenderer;

    private float _curDashCooltime;
    private bool _isDash = false;
    private bool _isHit = false;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_input._isDash && !_isDash && _curDashCooltime <= 0)
        {
            _isDash = true;
            _curDashCooltime = _dashCooltime;
            StartCoroutine(Dash());
        }

        if (_curDashCooltime > 0) 
        {
            _curDashCooltime -= Time.deltaTime;
            if (_curDashCooltime <= 0) _curDashCooltime = 0;
        }

        if (_input._isDash) 
        {
            print("switch");
        }
    }

    IEnumerator Dash()
    {
        Vector2 dir = _spriteRenderer.flipX ? transform.right * -1 : transform.right;
        _rigid.velocity = Vector2.zero;
        _rigid.AddForce(dir * _dashPower, ForceMode2D.Impulse);
        _rigid.gravityScale = 0;

        float time = 0;
        float afterTime = 0;
        float targetTime = Random.Range(0.02f, 0.06f);
        while (_isDash)
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

            if (time >= _dashTime)
            {
                _isDash = false;
            }
            yield return null;
        }
        _rigid.velocity = Vector2.zero;
        _rigid.gravityScale = 1;
    }

}

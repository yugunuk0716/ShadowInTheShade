using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;

    [SerializeField]
    private EnemyMovementSO _movementSO;

    [SerializeField]
    protected float _currentVelocity = 3;
    protected Vector2 _movementDirection;

    //�ӵ� ü������ ����
    public Action<float> OnVelocityChange;

    //�˹�������� üũ�ϴ� ����
    protected bool _isKnockBack = false;

    protected Coroutine _knockBackCo = null;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        
    }

    public void MoveAgent(Vector2 movementInput)
    {

        //_rigid.velocity = movement.normalized * movementSO.maxSpeed;
        if (movementInput.sqrMagnitude > 0)
        {
            if (Vector2.Dot(movementInput, _movementDirection) < 0)
            {
                _currentVelocity = 0; //���������� ����� �ٷ� �ӵ� ���̰� �ٽ� �����ϵ���
            }
            _movementDirection = movementInput.normalized;
        }

        _currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {

        if (movementInput.sqrMagnitude > 0)
        {
            _currentVelocity += _movementSO.aceleration * Time.deltaTime;
        }
        else
        {
            _currentVelocity -= _movementSO.deAceleration * Time.deltaTime;
        }

        return Mathf.Clamp(_currentVelocity, 0, _movementSO.maxSpeed);
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(_currentVelocity);

        //�˹���°� �ƴҰ�쿡�� �̵�
        if (!_isKnockBack)
            _rigid.velocity = _movementDirection * _currentVelocity;

    }

    public void StopImmediatelly()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if (!_isKnockBack)
        {
            _isKnockBack = true;
            _knockBackCo = StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }

    public void ResetKnockBack()
    {
        if (_knockBackCo != null)
        {
            StopCoroutine(_knockBackCo);
        }
        ResetKnockBackParam();
    }

    IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        _rigid.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParam();
    }

    private void ResetKnockBackParam()
    {
        _currentVelocity = 0;
        _rigid.velocity = Vector2.zero;
        _isKnockBack = false;
    }
}

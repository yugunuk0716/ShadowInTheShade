using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float _moveX { get; private set; }
    public float _moveY { get; private set; }
    public bool _isDash { get; private set; }
    public bool _isAction { get; private set; }
    public bool _isShadow { get; private set; }

    


    private void Awake()
    {

    }

    private void Update()
    {
        if (GameManager.TimeScale <= 0)
        {
            _moveX = 0;
            _moveY = 0;
            _isDash = false;
            _isAction = false;
            return;
        }

        
       
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");
        _isAction = Input.GetButtonDown("Fire1");
        _isShadow = Input.GetButtonDown("Switch");

       

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float moveX { get; private set; }
    public float moveY { get; private set; }
    public bool isDash { get; private set; }
    public bool isAction { get; private set; }
    public bool isShadow { get; private set; }

    


    private void Awake()
    {

    }

    private void Update()
    {
        if (GameManager.TimeScale <= 0)
        {
            moveX = 0;
            moveY = 0;
            isDash = false;
            isAction = false;
            return;
        }

        
       
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        isAction = Input.GetButtonDown("Fire1");
        isShadow = Input.GetButtonDown("Switch");

       

    }

    
}

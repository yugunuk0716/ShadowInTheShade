using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private int attackStack;

    private void Start()
    {
        attackStack = 0;
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if(playerInput.isAttack)
        {
            Attack();
            playerInput.isAttack = false;
        }
    }

    private void Attack()
    {
        if (attackStack <= 0)
            attackStack++;
        else
            attackStack = 0;

        GameManager.Instance.onPlayerAttack.Invoke(attackStack);
    }
}

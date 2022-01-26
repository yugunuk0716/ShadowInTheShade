using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnPlayerAttack.AddListener(Attack);
    }

    private void Attack()
    {
        //공격할때 해줄 것 
    }
}

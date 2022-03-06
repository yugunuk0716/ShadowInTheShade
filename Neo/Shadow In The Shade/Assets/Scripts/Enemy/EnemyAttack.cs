using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AI enemyAI;
    public Transform target;
    public Rigidbody2D rigid;
    public bool isAttacking = false;
    protected float attackDelay;

    protected virtual void Awake()
    {
        enemyAI = GetComponent<AI>();
    }

    protected virtual void Start()            
    {
        enemyAI.onStateEnter.AddListener(Attack);
        target = GameManager.Instance.player;
        //rigid = enemyAI.myRigid;
    }

    protected virtual void Attack()
    {

    }

  
}

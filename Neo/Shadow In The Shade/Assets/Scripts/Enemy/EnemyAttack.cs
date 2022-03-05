using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public AI enemyAI;
    protected float attackDelay;

    private void Awake()
    {
        enemyAI = GetComponent<AI>();
    }

    private void Start()
    {
        enemyAI.onStateEnter.AddListener(Attack);
    }

    private void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public virtual IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackDelay);
    }
}

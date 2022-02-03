using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    private EnemyAI _enemyBrain;

    [field: SerializeField]
    public float attackDelay { get; set; } = 1;

    protected bool _waitBeforeNextAttack;

    private void Awake()
    {
        _enemyBrain = GetComponent<EnemyAI>();
        AwakeChild();
    }

    public virtual void AwakeChild()
    {
        //do nothing here;
    }

    public abstract void Attack(int damage);

    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(attackDelay);
        _waitBeforeNextAttack = false;
    }

    protected GameObject GetTarget()
    {
        return _enemyBrain.target;
    }
}

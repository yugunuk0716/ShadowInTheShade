using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Tackle : MonoBehaviour, IState
{
    Enemy enemy;
    ITacklable tacklable;

    int originLayer;
    readonly int targetLayer = 9;

    private Collider2D coll;


    public void OnEnter()
    {
        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }

        if (coll == null)
        {
            coll = GetComponent<BoxCollider2D>();
        }

        if (tacklable == null)
        {
            tacklable = GetComponentInParent<ITacklable>();
        }
        originLayer = enemy.gameObject.layer;
        enemy.gameObject.layer = targetLayer;

        if (enemy != null && !enemy.isAttack)
        {
            tacklable.SetTackle(true);
            Vector3 vec = GameManager.Instance.player.position - transform.position;
            enemy.Anim.SetFloat("MoveX", vec.x); // Mathf.Clamp(vec.x, -1f, 1f));
            enemy.Anim.SetFloat("MoveY", vec.y); //Mathf.Clamp(vec.y, -1f, 1f));
            //enemy.move.rigid.velocity = Vector2.zero;
            enemy.move.OnMove(vec.normalized, 9f);
            enemy.Anim.SetBool("isTackle", true);
        }

       
    }

    public void OnEnd()
    {

    }

    public void TackleEnd() 
    {
        if (enemy != null)
        {
            Invoke(nameof(AttackReset), 1.5f);
            enemy.Anim.SetBool("isTackle", false);
            enemy.Anim.SetFloat("MoveX", 0);
            enemy.Anim.SetFloat("MoveY", 0);
            enemy.gameObject.layer = originLayer;
        }
    }

    void AttackReset()
    {
        tacklable.SetTackle(false);
    }




}

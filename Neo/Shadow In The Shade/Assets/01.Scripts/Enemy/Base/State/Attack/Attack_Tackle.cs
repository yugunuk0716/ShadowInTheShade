using DG.Tweening;
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
    AttackArea atkArea;

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
        enemy.move.rigid.velocity = Vector3.zero;

        StartCoroutine(TackleRoutine());

       
    }

    public void OnEnd()
    {

    }

    IEnumerator TackleRoutine()
    {
        if (enemy != null && !enemy.isAttack)
        {
            tacklable.SetTackle(true);
            Vector3 vec = GameManager.Instance.player.position - transform.position;
            atkArea = PoolManager.Instance.Pop("AttackArea") as AttackArea;
            atkArea.transform.position = transform.position;
            atkArea.Lr.SetPosition(0, Vector3.zero);
            float a = 0;
            while (a <= 4)
            {
                a += 0.02f;
                print($"{a} && {vec.normalized * a}");
                atkArea.Lr.SetPosition(1, vec.normalized * a);
                yield return new WaitForSeconds(0.001f);

            }
            

            yield return new WaitForSeconds(.1f);

            enemy.Anim.SetFloat("MoveX", vec.x); // Mathf.Clamp(vec.x, -1f, 1f));
            enemy.Anim.SetFloat("MoveY", vec.y); //Mathf.Clamp(vec.y, -1f, 1f));
            enemy.move.OnMove(vec.normalized, 10f);

            yield return new WaitForSeconds(.5f);
            enemy.move.rigid.velocity = Vector2.zero;
            
            enemy.Anim.SetBool("isTackle", true);
        }

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
            PoolManager.Instance.Push(atkArea);
        }
    }

    void AttackReset()
    {
        tacklable.SetTackle(false);
    }




}

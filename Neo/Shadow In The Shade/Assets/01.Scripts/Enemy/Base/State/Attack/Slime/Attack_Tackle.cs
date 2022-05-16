using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Attack_Tackle : MonoBehaviour, IState
{
    public bool canAttack;

    Enemy enemy;
    ITacklable tacklable;

    //int originLayer;
   // readonly int targetLayer = 9;

    private Collider2D coll;
    AttackArea atkArea;

    Coroutine tackleRoutine;
 

    //AIDestinationSetter destinationSetter;
    //Seeker seeker;
    //AIPath path;

    
    public void OnEnter()
    {
        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
            enemy.OnReset.AddListener(AttackReset);
        }

        if (coll == null)
        {
            coll = GetComponent<BoxCollider2D>();
        }

        if (tacklable == null)
        {
            tacklable = GetComponentInParent<ITacklable>();
        }

        //if (destinationSetter == null)
        //    destinationSetter = GetComponentInParent<AIDestinationSetter>();

        //if (seeker == null)
        //    seeker = GetComponentInParent<Seeker>();

        //if (path == null)
        //    path = GetComponentInParent<AIPath>();

        if (tackleRoutine != null)
        {
            enemy.destinationSetter.target = null;
            enemy.SetAttack(true);
            StopCoroutine(tackleRoutine);
            tacklable.SetTackle(false);
            enemy.Anim.SetBool("isTackle", false);
            enemy.Anim.SetFloat("MoveX", 0);
            enemy.Anim.SetFloat("MoveY", 0);
            //enemy.gameObject.layer = originLayer;
           
            enemy.IsHit = false;
        }


        //originLayer = enemy.gameObject.layer;
        //enemy.gameObject.layer = targetLayer;
        enemy.Move.rigid.velocity = Vector3.zero;

        enemy.destinationSetter.target = null;
        enemy.SetAttack(false);

        if (tackleRoutine == null)
            tackleRoutine = StartCoroutine(TackleRoutine());


       
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
                atkArea.Lr.SetPosition(1, vec.normalized * a);
                yield return new WaitForSeconds(0.001f);
                if (!canAttack)
                    yield break;

            }
         

            yield return new WaitForSeconds(.1f);
            PoolManager.Instance.Push(atkArea);
            enemy.Anim.SetFloat("MoveX", vec.x); // Mathf.Clamp(vec.x, -1f, 1f));
            enemy.Anim.SetFloat("MoveY", vec.y); //Mathf.Clamp(vec.y, -1f, 1f));
            enemy.Move.OnMove(vec.normalized, 10f);

            yield return new WaitForSeconds(.5f);
            enemy.Move.rigid.velocity = Vector2.zero;
            
            enemy.Anim.SetBool("isTackle", true);
            enemy.SetAttack(true);
            tackleRoutine = null;
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
         
            //enemy.gameObject.layer = originLayer;
            PoolManager.Instance.Push(atkArea);
        }
    }

    void AttackReset()
    {
        enemy.destinationSetter.target = GameManager.Instance.player;
        enemy.SetAttack(true);

        tacklable.SetTackle(false);
        canAttack = false;
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Dice : MonoBehaviour, IState
{

    Boss_Dice dice;

    Coroutine dashRoutine;
    AttackArea atkArea;

    private bool isCollision = false;
    private bool canCollision = false;

    private readonly LayerMask whatIsCollisionable = LayerMask.GetMask("Wall");

    public void OnEnter()
    {

        if(dice == null)
        {
            dice = GetComponent<Boss_Dice>();
        }
        
        if (dashRoutine == null)
        {
            dashRoutine = StartCoroutine(DashRoutine());
        }

        canCollision = true;
        isCollision = false;

    }


    public void OnEnd()
    {
        if (dashRoutine != null && !dice.isMoving)
        {
            dice.Anim.SetBool("isDash", false);
            StopCoroutine(dashRoutine);
            dashRoutine = null;
            PoolManager.Instance.Push(atkArea);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        print($"{canCollision} {collision.gameObject.CompareTag("Wall")} {collision.gameObject.name}");
        if (canCollision && collision.gameObject.CompareTag("Wall"))
        {
            print("들어와여...");
            isCollision = true;
            canCollision = false; 
        }
      
       
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (canCollision && collision.gameObject.CompareTag("Wall"))
    //    {
    //        print("들어와여...");
    //        isCollision = true;
    //        canCollision = false;
    //    }


    //}



    IEnumerator DashRoutine()
    {
        if (dice != null)
        {
            Vector3 vec = GameManager.Instance.player.position - transform.position;
            atkArea = PoolManager.Instance.Pop("AttackArea") as AttackArea;
            atkArea.transform.position = transform.position;
            atkArea.Lr.SetPosition(0, Vector3.zero);
            print("????왜 나에대한 기준만 엄격한건데!!!!!??");
            dice.isMoving = true;
            if (vec.x > vec.y && vec.x > 0)
            {
                dice.MyRend.flipX = true;
            }
            else
            {
                dice.MyRend.flipX = false;
            }
            float a = 0;
            while (a <= 4)
            {
                a += 0.02f;
                atkArea.Lr.SetPosition(1, vec.normalized * a);
                yield return new WaitForSeconds(0.001f);

            }


            yield return new WaitForSeconds(.1f);

            dice.Anim.SetFloat("MoveX", vec.x); // Mathf.Clamp(vec.x, -1f, 1f));
            dice.Anim.SetFloat("MoveY", vec.y); //Mathf.Clamp(vec.y, -1f, 1f));
            dice.Anim.SetBool("isDash", true);
            dice.Move.OnMove(vec.normalized, 8f);
            yield return new WaitForSeconds(.1f);
            //canCollision = true;
            print(canCollision && isCollision);
            yield return new WaitUntil(() => isCollision);


            dice.isMoving = false;
            dice.Anim.SetBool("isDash", false);
            dice.Move.rigid.velocity = Vector2.zero;
            PoolManager.Instance.Push(atkArea);
            
        }
    }


    
}

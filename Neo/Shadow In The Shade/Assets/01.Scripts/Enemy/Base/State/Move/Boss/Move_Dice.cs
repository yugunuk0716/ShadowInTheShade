using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Dice : MonoBehaviour, IState
{

    Boss_Dice dice;

    RaycastHit2D hit2D;

    Coroutine dashRoutine;
    AttackArea atkArea;
    Vector3 originPos;
    Collider2D lastColl;


    private LayerMask whatIsCollisionable;
    Vector3 v;

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

        if(whatIsCollisionable == default(LayerMask))
        {
            whatIsCollisionable = LayerMask.GetMask("Wall");
        }



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


    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    hit2D = Physics2D.Raycast(transform.position, transform.position - collision.transform.position, 0.01f, LayerMask.GetMask("Wall"));
    //    if (hit2D.collider == null)
    //    {
    //        isCollision = true;
    //    }

    //}



    IEnumerator DashRoutine()
    {
        if (dice != null)
        {
            Vector3 playerPos = GameManager.Instance.player.position;
            Vector3 vec = playerPos - transform.position;
            Vector3 attackDir = Vector3.zero;
            atkArea = PoolManager.Instance.Pop("AttackArea") as AttackArea;
            atkArea.transform.position = transform.position;
            atkArea.Lr.widthMultiplier = 3f;
            atkArea.Lr.SetPosition(0, Vector3.zero);
            dice.isMoving = true;
            if (vec.x > vec.y && vec.x > 0)
            {
                dice.MyRend.flipX = true;
            }
            else
            {
                dice.MyRend.flipX = false;
            }
            if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
            {
                if (transform.position.x < playerPos.x)
                {
                    attackDir = Vector2.right;
                }
                else
                {
                    attackDir = Vector2.left;
                }
            }
            else
            {
                if (transform.position.y < playerPos.y)
                {
                    attackDir = Vector2.up;
                }
                else
                {
                    attackDir = Vector2.down;
                }
            }

            float a = 0;
            while (a <= 4)
            {
                a += 0.02f;
                atkArea.Lr.SetPosition(1, attackDir * a);
                yield return new WaitForSeconds(0.001f);

            }


            yield return new WaitForSeconds(.1f);

            dice.Anim.SetFloat("MoveX", vec.x); // Mathf.Clamp(vec.x, -1f, 1f));
            dice.Anim.SetFloat("MoveY", vec.y); //Mathf.Clamp(vec.y, -1f, 1f));
            dice.Anim.SetBool("isDash", true);
            PoolManager.Instance.Push(atkArea);
            originPos = transform.position;
            dice.Move.OnMove(attackDir, 10f);
            //yield return new WaitForSeconds(2f);
            //canCollision = true;
            //print(canCollision && isCollision);

            v = (Vector3)dice.Move.rigid.velocity.normalized;
            while (true)
            {
                Collider2D coll = Physics2D.OverlapBox(transform.position + v, new Vector2(4f, 4f), 45, whatIsCollisionable);
                if(coll != null)
                {
                    if(lastColl != null)
                    {
                        if (coll == lastColl)
                        {
                            yield return null;
                            continue;
                        }
                    }

                    lastColl = coll;
                    SlimePillar sp = coll.GetComponent<SlimePillar>();

                    if(sp != null)
                    {
                        dice.CurrHP += sp.healAmount;
                        PoolManager.Instance.Push(sp);
                    }
                    GameManager.Instance.feedBackPlayer.PlayFeedback();
                    break;
                }

                //if ((transform.position - originPos).sqrMagnitude > 100)
                //    break;

                yield return new WaitForSeconds(.5f);
            }
            


            dice.isMoving = false;
            dice.Anim.SetBool("isDash", false);
            dice.Move.rigid.velocity = Vector2.zero;
            
        }
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(dice != null)
        {
            Gizmos.DrawWireCube(transform.position + (Vector3)dice.Move.rigid.velocity.normalized, new Vector2(4f, 4f));
        }
    }
#endif


}

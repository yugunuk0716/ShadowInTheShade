using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Attack_Dice : MonoBehaviour, IState
{

    Boss_Dice dice;

    Coroutine crashRoutine;
    AttackArea atkArea;
    Vector3 dir = Vector3.zero;
    Vector3 attackDir = Vector3.zero;


    public float attackAngle = 60f;
    public float attackDist = 6f;

    public void OnEnter()
    {
        if (dice == null)
            dice = GetComponent<Boss_Dice>();

        switch (dice.diceType)
        {
            case DiceType.Mk1:
                attackAngle = 60f;
                attackDist = 6f;
                break;
            case DiceType.Mk2:
                attackAngle = 30f;
                attackDist = 3f;
                break;
            case DiceType.Mk3:
                attackAngle = 15f;
                attackDist = 1.5f;
                break;
        }


        if(crashRoutine == null)
        {
            crashRoutine = StartCoroutine(Crash());
        }
    }

    public void OnEnd()
    {
        
        if(crashRoutine != null && !dice.isAttacking)
        {
            dice.Anim.SetBool("isCrash", false);
            StopCoroutine(crashRoutine);
            crashRoutine = null;
            PoolManager.Instance.Push(atkArea);
        }
    }

    IEnumerator Crash() 
    {
        float a = 1;
        Vector3 playerPos = GameManager.Instance.player.position;
        dir = (playerPos - gameObject.transform.position).normalized;
        attackDir = Vector3.zero;

        dice.isAttacking = true;

        atkArea = PoolManager.Instance.Pop("AttackArea") as AttackArea;
        atkArea.Lr.widthMultiplier = 5f;
        atkArea.transform.position = transform.position;
        atkArea.Lr.SetPosition(0, Vector2.zero);
        atkArea.Lr.SetPosition(1, Vector2.zero);

        if (dir.x > dir.y && dir.x > 0)
        {
            dice.MyRend.flipX = true;
        }
        else
        {
            dice.MyRend.flipX = false;
        }


        if (Mathf.Abs(transform.position.x - playerPos.x) > Mathf.Abs(transform.position.y - playerPos.y))
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


        while (a <= 4)
        {
            a += 0.02f;
            atkArea.Lr.SetPosition(1, attackDir * a);
            yield return new WaitForSeconds(0.001f);

        }

        yield return new WaitForSeconds(.1f);
        dice.Anim.SetFloat("MoveX", dir.x);
        dice.Anim.SetFloat("MoveY", dir.y);
        dice.Anim.SetBool("isCrash", true);

        yield return new WaitForSeconds(1.2f);
        GameManager.Instance.feedBackPlayer.PlayFeedback();
        dir =  (playerPos - gameObject.transform.position).normalized;
        if (IsInSight(dir, attackDir))
        {
            //print("?????? ??????..");
            IDamagable d = GameManager.Instance.player.GetComponent<IDamagable>();
            GameManager.Instance.feedBackPlayer.PlayFeedback();
            d.GetHit(dice.enemyData.damage, dice.enemyData.hitNum);
            d.KnockBack(-dir, 10f, .1f);
        }



        yield return null;
    }

  
    private bool IsInSight(Vector3 targetDir, Vector3 lookDir)
    {
        float dot = Vector3.Dot(targetDir, -lookDir);
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;

        

        if (theta <= attackAngle && Mathf.Pow(attackDist, 2f) > (transform.position - GameManager.Instance.player.position).sqrMagnitude)
        {
            return true;
        }

        return false;
    }




}

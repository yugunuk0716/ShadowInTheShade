using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Fire : MonoBehaviour, IState
{
    Enemy enemy;
    private GameObject dieParticle;
    GameObject obj;

    public void OnEnter()
    {
        if (dieParticle == null)
        {
            dieParticle = Resources.Load<GameObject>("Fire Die Effect");
        }

        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }

       

        enemy.Anim.SetBool("isAttack", true);
    }

    public void AttackEnd()
    {
        if (enemy != null)
        {
            enemy.Anim.SetBool("isAttack", false);
            obj = Instantiate(dieParticle);
            print(this.transform.position);
            obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1f, -5f);
            enemy.CurrHP = 0;
            enemy.GetHit(enemy.CurrHP);
        }
    }

    public void OnEnd()
    {

    }


}

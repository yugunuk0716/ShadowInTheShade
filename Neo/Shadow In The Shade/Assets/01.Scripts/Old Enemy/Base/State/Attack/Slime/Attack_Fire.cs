using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Fire : MonoBehaviour, IState
{
    OldEnemy enemy;
    //private GameObject dieParticle;
    FireParticle obj;

    public void OnEnter()
    {
        //if (dieParticle == null)
        //{
        //    dieParticle = Resources.Load<GameObject>("Fire Die Effect");
        //}

        if (enemy == null)
        {
            enemy = GetComponentInParent<OldEnemy>();
        }

       

        enemy.Anim.SetBool("isAttack", true);
    }

    public void AttackEnd()
    {
        if (enemy != null)
        {
            enemy.Anim.SetBool("isAttack", false);
            obj = PoolManager.Instance.Pop("Fire Die Effect") as FireParticle;
            obj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1f, -5f);
            enemy.CurrHP = 0;
        }
    }

    

    public void OnEnd()
    {

    }


}

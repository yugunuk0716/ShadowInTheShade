using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Tackle : MonoBehaviour, IState
{
    Enemy enemy;
    ITacklable tacklable;

    public void OnEnter()
    {
        if(enemy == null)
        {
            enemy = GetComponent<Enemy>();
        }

        if (tacklable == null)
        {
            tacklable = GetComponent<ITacklable>();
        }

        if (enemy != null)
        {
            tacklable.SetTackle(true);
            Vector3 vec = (GameManager.Instance.player.position - transform.position).normalized;
            enemy.Anim.SetFloat("MoveX", vec.x);// Mathf.Clamp(vec.x, -1f, 1f));
            enemy.Anim.SetFloat("MoveY", vec.y);//Mathf.Clamp(vec.y, -1f, 1f));
            enemy.Anim.SetBool("isTackle", true);
        }
    }

    public void OnEnd()
    {
        if (enemy != null)
        {
            Invoke(nameof(AttackReset), 1);
            enemy.Anim.SetBool("isTackle", false);
        }
    }

    void AttackReset()
    {
        tacklable.SetTackle(false);
    }

    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : PoolableMono
{
    private Animator anim;
    public Animator Anim
    {
        get
        {
            if (anim == null)
            {
                anim = GetComponent<Animator>();
            }

            return anim;
        }
    }

    PlayerAnimation playerAnim;

    //�ϴ� ��ǥ ȸ������ ũ��Ƽ�������� ������ �ް�

    //��ǥ, ���⺤��, �Ұ� ������ �־�ߴ�
    public void SetDamageEffect(Vector3 pos, Vector3 dir, bool isCritical) 
    {

        if (playerAnim == null)
        {
            playerAnim = GameManager.Instance.player.GetComponentInChildren<PlayerAnimation>();
        }
        if (isCritical)
        {
            Anim.SetTrigger("isCritical");
        }
        transform.position = pos + (dir.normalized * -1.1f);


        float a = GameManager.Instance.player.position.y + 0.5f < pos.y  ? -1 : 0;
        float b = GameManager.Instance.player.position.x < pos.x ? 1 : 0;
        transform.rotation = Quaternion.Euler(0,0, (Mathf.Acos(Vector3.Dot(playerAnim.lastMoveDir, dir )) + a + b) * Mathf.Rad2Deg);
    }

    public virtual void PushInPool()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        Anim.SetBool("isCritical", false);
    }
}

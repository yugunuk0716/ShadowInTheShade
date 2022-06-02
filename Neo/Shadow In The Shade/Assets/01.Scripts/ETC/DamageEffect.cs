using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : PoolableMono
{
    private void OnParticleSystemStopped()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }
}

//private Animator anim;
//public Animator Anim
//{
//    get
//    {
//        if (anim == null)
//        {
//            anim = GetComponent<Animator>();
//        }

//        return anim;
//    }
//}

//PlayerAnimation playerAnim;

////일단 좌표 회전값과 크리티컬인지를 변수로 받고

////좌표, 방향벡터, 불값 순서로 넣어야댐
//public void SetDamageEffect(Vector3 pos, Vector3 dir, bool isCritical) 
//{

//    if (playerAnim == null)
//    {
//        playerAnim = GameManager.Instance.player.GetComponentInChildren<PlayerAnimation>();
//    }
//    if (isCritical)
//    {
//        Anim.SetTrigger("isCritical");
//    }
//    transform.position = pos;


//    //float a = GameManager.Instance.player.position.y < pos.y + 0.2f  ? -1 : 0;
//    //print($"{Mathf.Abs(GameManager.Instance.player.position.y - pos.y - 0.2f) }& { Mathf.Abs(GameManager.Instance.player.position.x - pos.x)}");
//    //if (Mathf.Abs(GameManager.Instance.player.position.y - pos.y - 0.2f) < Mathf.Abs(GameManager.Instance.player.position.x - pos.x))
//    //{
//    //    a = GameManager.Instance.player.position.x < pos.x ? 1 : 0;
//    //}
//    Vector2 v2 = (GameManager.Instance.player.position - pos).normalized;
//    float a = 0;
//    if(GameManager.Instance.player.position.y < pos.y + 0.1f)// || GameManager.Instance.player.position.x < pos.x)
//    {
//        a = -1;
//    }
//    transform.rotation = Quaternion.Euler(0, 0,  (Mathf.Acos(Vector3.Dot(playerAnim.lastMoveDir, dir)) - v2.x + v2.y + a) * Mathf.Rad2Deg);
//}

//public virtual void PushInPool()
//{
//    PoolManager.Instance.Push(this);
//}

//public override void Reset()
//{
//    Anim.SetBool("isCritical", false);
//}
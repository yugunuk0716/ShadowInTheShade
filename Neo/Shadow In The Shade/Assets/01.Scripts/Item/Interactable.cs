using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : PoolableMono
{
    protected bool used = false;

    public abstract void Use(GameObject target);


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;
        
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public virtual void PushChestInPool()
    {
        PoolManager.Instance.Push(this);
    }

    public virtual void Popup(Vector3 pos)// 이 함수는 스테이지 클리어시 풀에서 상자 꺼내서 실행하면 됨
    {
        
    }

  

}

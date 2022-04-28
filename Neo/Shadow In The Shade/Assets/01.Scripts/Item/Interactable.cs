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

    public virtual void Popup(Vector3 pos)// �� �Լ��� �������� Ŭ����� Ǯ���� ���� ������ �����ϸ� ��
    {
        
    }

  

}

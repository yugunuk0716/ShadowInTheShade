using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : PoolableMono
{
    protected bool used = false;

    public abstract void Use(GameObject target);
    private Camera main;

    protected virtual void Awake()
    {
        main = Camera.main;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;

        //UIManager.Instance.ShowInteractableGuideImage(main.WorldToScreenPoint(transform.position) + main.transform.position);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        //UIManager.Instance.CloseInteractableGuideImage();
    }

    public virtual void PushChestInPool()
    {
        PoolManager.Instance.Push(this);
    }

    public virtual void Popup(Vector3 pos)
    {
        
    }

  

}

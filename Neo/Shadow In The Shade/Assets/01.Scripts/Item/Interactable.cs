using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : PoolableMono
{
    protected bool used = false;

    public abstract void Use(GameObject target);
    private Camera main;

    protected virtual void Start()
    {
        main = Camera.main;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;
        print(UIManager.Instance == null);
        print(Camera.main == null);

        UIManager.Instance.ShowInteractableGuideImage(Camera.main.WorldToScreenPoint(GameManager.Instance.player.position) + new Vector3(0f, 0.5f, 0f));
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        UIManager.Instance.CloseInteractableGuideImage();
    }

    public virtual void PushChestInPool()
    {
        PoolManager.Instance.Push(this);
    }

    public virtual void Popup(Vector3 pos)
    {
        
    }

  

}

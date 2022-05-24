using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoDoor : Interactable
{
   

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }


    public override void Use(GameObject target)
    {
        if (used)
        {
            print("¿ÀÇÂ ½ÇÆÐ");
            return;
        }
        NeoRoomManager.instance.LoadNextRoom();
        PoolManager.Instance.Push(this);
        used = true;
    }

    public override void Reset()
    {
        used = false;
    }
}

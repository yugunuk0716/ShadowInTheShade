using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : PoolableMono
{
    protected bool used = false;

    public abstract void Use(GameObject target);


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            //���⼭ UI ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //UI �����
        }
    }

}

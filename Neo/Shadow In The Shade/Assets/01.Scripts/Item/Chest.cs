using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Chest : Interactable
{
    public Rarity rarity;

    private BoxCollider2D boxCol;
    private Animator anim;
    private Rigidbody2D rigid;
    private ItemSO targetItem;

    private bool canUse = false;

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
        canUse = true;
      
    }

   
    

    public override void Use(GameObject target)
    {
        if (!canUse || used)
        {
            print("���� ����");
            print(!canUse);
            print(used);
            return; 
        }

        print("����");
        used = true;
        anim.SetTrigger("open");
        boxCol.enabled = false;
        //���⼭ ������ �޾ƿͼ� ���
        Item item = PoolManager.Instance.Pop("Item Temp") as Item;
        item.transform.position = transform.position - new Vector3(.1f, 0, 0);
        item.transform.DOMove(transform.position - new Vector3(1, 1), 1f);
        item.canUse = true;
        item.Init(rarity);
        Invoke(nameof(PushChestInPool), 3f);
    }


    public override void PushChestInPool()
    {
        base.PushChestInPool();
    }

    public override void Popup(Vector3 pos)// �� �Լ��� �������� Ŭ����� Ǯ���� ���� ������ �����ϸ� ��
    {
        transform.position = pos;
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        OpenDelay();
    }

    public void OpenDelay()
    {
        //rigid.gravityScale = 1f;
        //yield return new WaitForSeconds(1f);
        //rigid.gravityScale = 0f;
        boxCol.isTrigger = true;
        rigid.velocity = Vector2.zero;
        canUse = true;
    }

    public override void Reset()
    {
        canUse = false;
        used = false;
        boxCol.enabled = true;
        anim.ResetTrigger("open");
    }
}

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
    private Rarity rarity;
    private ItemSO targetItem;

    private bool canUse = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
    }

    private void Start()
    {
        StageManager.Instance.onBattleEnd.AddListener(() => {  });
    }

 

    public override void Use(GameObject target)
    {
        if (!canUse || used)
        {
            print("오픈 실패");
            return; 
        }

        print("오픈");
        used = true;
        anim.SetTrigger("open");

        //여기서 아이템 받아와서 드랍
        Item item = PoolManager.Instance.Pop("Item Temp") as Item;
        item.transform.position = transform.position - new Vector3(.1f, 0, 0);
        item.transform.DOMove(transform.position - new Vector3(1, 1), 1f);
        item.Init(rarity);
        Invoke(nameof(PushChestInPool), 3f);
    }


    public override void PushChestInPool()
    {
        base.PushChestInPool();
    }

    public override void Popup(Vector3 pos)// 이 함수는 스테이지 클리어시 풀에서 상자 꺼내서 실행하면 됨
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
        anim.ResetTrigger("open");
    }
}

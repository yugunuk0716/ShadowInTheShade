using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public ItemSO itemSO;


    private BoxCollider2D boxCol;
    private Animator anim;
    private Rigidbody2D rigid;

    public bool canUse = false;


    private SpriteRenderer sr;
    public SpriteRenderer Sr
    {
        get 
        {
            if(sr == null)
                sr = GetComponent<SpriteRenderer>();
            return sr; 
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
    }

    private void OnEnable()
    {
        
    }

    public void Init(Rarity rarity)
    {
        itemSO = ItemManager.Instance.PickItem(rarity);

        Sr.sprite = itemSO.itemSprite;
    }

    public override void Use(GameObject target)
    {
        if (!canUse || used)
        {
            print("아이템오픈 실패");
            return;
        }

        print("아이템오픈");
        used = true;
        canUse = false;

        ItemManager.Instance.AddingItem(itemSO);
        PoolManager.Instance.Push(this);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //UI 출력
            if(itemSO != null)
            {
                UIManager.Instance.ShowToolTip($"{itemSO.itemAbility} \n {itemSO.itemComment}", itemSO.itemSprite);
            }
            base.OnTriggerEnter2D(collision);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //UI 출력
            UIManager.Instance.CloseTooltip();
            base.OnTriggerExit2D(collision);
        }
    }

    public override void Reset()
    {
        canUse = false;
        used = false;
        Sr.sprite = null;
        itemSO = null;
    }

}

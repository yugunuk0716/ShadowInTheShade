using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public ItemSO itemSO;

    public SpriteRenderer bg;
    public bool canUse = false;

    private Dictionary<Rarity, Color> colorDic = new Dictionary<Rarity, Color>();

    private BoxCollider2D boxCol;
    private Animator anim;
    private Rigidbody2D rigid;

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

    //989898   0069A3   6E2F8E   FFCD28
    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
        used = false;
        base.Awake();

        colorDic.Add(Rarity.Normal, Color.white);
        colorDic.Add(Rarity.Rare, new Color(0f, 105f, 163f));
        colorDic.Add(Rarity.Unique, new Color(110f, 47f, 142f));
        colorDic.Add(Rarity.Legendary, new Color(255f, 205f, 40f));

    }

    private void OnEnable()
    {
        
    }

    public void Init(Rarity rarity)
    {
        itemSO = ItemManager.Instance.PickItem(rarity);
        bg.color = colorDic[rarity];
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

        //여기서 상점 갔던 여부 확인하고 안갔다면 2번을 상점으로 설정하고 생성 해주면 되고 다른 방은 랜덤으로 굴리는데 이제 Boss방이 뜰수 있나 없나 이정도로

      
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
                UIManager.Instance.ShowToolTip($"\n <b>{itemSO.itemName}</b> \n  \n {itemSO.itemAbility} \n {itemSO.itemComment}", itemSO.itemSprite);
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            //UI 출력
            UIManager.Instance.CloseTooltip();
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

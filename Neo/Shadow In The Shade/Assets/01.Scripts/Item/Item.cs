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
            print("�����ۿ��� ����");
            return;
        }

        print("�����ۿ���");
        used = true;
        canUse = false;

        //���⼭ ���� ���� ���� Ȯ���ϰ� �Ȱ��ٸ� 2���� �������� �����ϰ� ���� ���ָ� �ǰ� �ٸ� ���� �������� �����µ� ���� Boss���� ��� �ֳ� ���� ��������

      
        ItemManager.Instance.AddingItem(itemSO);
        PoolManager.Instance.Push(this);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //UI ���
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

            //UI ���
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

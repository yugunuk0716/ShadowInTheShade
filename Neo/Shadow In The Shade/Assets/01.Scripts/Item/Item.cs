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

    protected override void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
        base.Awake();
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
            print("�����ۿ��� ����");
            return;
        }

        print("�����ۿ���");
        used = true;
        canUse = false;

        //���⼭ ���� ���� ���� Ȯ���ϰ� �Ȱ��ٸ� 2���� �������� �����ϰ� ���� ���ָ� �ǰ� �ٸ� ���� �������� �����µ� ���� Boss���� ��� �ֳ� ���� ��������

        NeoDoor nDoor = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
        nDoor.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.left;
        NeoDoor nDoor2 = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
        nDoor2.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.right;
        nDoor.pairDoor = nDoor2;
        nDoor2.pairDoor = nDoor;
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
                UIManager.Instance.ShowToolTip($"{itemSO.itemAbility} \n {itemSO.itemComment}", itemSO.itemSprite);
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

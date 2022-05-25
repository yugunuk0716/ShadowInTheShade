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
            print("아이템오픈 실패");
            return;
        }

        print("아이템오픈");
        used = true;
        canUse = false;

        //여기서 상점 갔던 여부 확인하고 안갔다면 2번을 상점으로 설정하고 생성 해주면 되고 다른 방은 랜덤으로 굴리는데 이제 Boss방이 뜰수 있나 없나 이정도로

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
            //UI 출력
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

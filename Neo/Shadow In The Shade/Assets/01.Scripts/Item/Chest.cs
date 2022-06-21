using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public struct ItemTable
{
    public Rarity rarity;
    public float nonePercentage;
    public float normalPercentage;
    public float rarePercentage;
    public float epicPercentage;
    public float legendaryPercentage;

    public ItemTable(Rarity rarity, float nonePercentage,  float normalPercentage, float rarePercentage, float epicPercentage, float legendaryPercentage)
    {
        this.rarity = rarity;
        this.nonePercentage = nonePercentage;
        this.normalPercentage = normalPercentage;
        this.rarePercentage = rarePercentage;
        this.epicPercentage = epicPercentage;
        this.legendaryPercentage = legendaryPercentage;
    }

}


public class Chest : Interactable
{
    public Rarity rarity;

    private BoxCollider2D boxCol;
    private Animator anim;
    private Rigidbody2D rigid;
    private ItemSO targetItem;

    private bool canUse = false;

    //환생 횟수
    //환생 보정치
    //기본 확률 테이블

    private Dictionary<Rarity, float> correctionNumberDictionary = new Dictionary<Rarity, float>();
    
    private ItemTable normalBox = new ItemTable(Rarity.Normal, 30f, 50f, 20f, 0f, 0f);
    private ItemTable rareBox = new ItemTable(Rarity.Rare, 20f, 25f, 55f, 0f, 0f);
    private ItemTable epicBox = new ItemTable(Rarity.Unique, 9f, 5f, 15f, 70f, 1f);
    private ItemTable legendaryBox = new ItemTable(Rarity.Legendary, 0f, 20f, 40f, 25f, 15f);

    private Dictionary<Rarity, ItemTable> chestPercentageDictionary = new Dictionary<Rarity, ItemTable>();

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
        canUse = true;

        correctionNumberDictionary.Add(Rarity.Normal, 0f);
        correctionNumberDictionary.Add(Rarity.Rare, -5f);
        correctionNumberDictionary.Add(Rarity.Unique, 0f);
        correctionNumberDictionary.Add(Rarity.Legendary, 0.3f);


        chestPercentageDictionary.Add(Rarity.Normal, normalBox);
        chestPercentageDictionary.Add(Rarity.Rare, rareBox);
        chestPercentageDictionary.Add(Rarity.Unique, epicBox);
        chestPercentageDictionary.Add(Rarity.Legendary, legendaryBox);

    }

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
        if (!canUse || used)
        {
            print(!canUse);
            print(used);
            return; 
        }

        used = true;
        anim.SetTrigger("open");
        boxCol.enabled = false;
        //여기서 아이템 받아와서 드랍
        Item item = PoolManager.Instance.Pop("Item Temp") as Item;
        if (StageManager.Instance.currentRoom.obstacles != null)
        {
            StageManager.Instance.currentRoom.obstacles.SetActive(false);
        }
        NeoRoomManager.instance.doorList.ForEach(door => { door.gameObject.SetActive(true); door.SetDoor(true); });
        item.transform.position = transform.position - new Vector3(.1f, 0, 0);
        item.transform.DOMove(transform.position - new Vector3(1, 1), 1f);
        item.canUse = true;
        item.Init(rarity);
        Invoke(nameof(PushChestInPool), 1f);
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
        boxCol.enabled = true;
        anim.ResetTrigger("open");
    }
}

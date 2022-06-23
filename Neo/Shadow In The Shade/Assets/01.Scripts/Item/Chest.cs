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

    public Dictionary<Rarity, float> percentDictionary;

    private Dictionary<Rarity, float> correctionNumberDictionary;


    public float GetPercent(Rarity rarity)
    {
        if (rarity.Equals(Rarity.None))
        {
            return nonePercentage;
        }
        return percentDictionary[rarity];// + StageManager.Instance.rebirthCount * correctionNumberDictionary[rarity];
    }
    

    public ItemTable(Rarity rarity,  float normalPercentage, float rarePercentage, float epicPercentage, float legendaryPercentage)
    {
        percentDictionary = new Dictionary<Rarity, float>();
        correctionNumberDictionary = new Dictionary<Rarity, float>();

        correctionNumberDictionary.Add(Rarity.Normal, 0f);
        correctionNumberDictionary.Add(Rarity.Rare, -5f);
        correctionNumberDictionary.Add(Rarity.Unique, 0f);
        correctionNumberDictionary.Add(Rarity.Legendary, 0.3f);

        float normal = normalPercentage + StageManager.Instance.rebirthCount * correctionNumberDictionary[Rarity.Normal];
        float rare = rarePercentage + StageManager.Instance.rebirthCount * correctionNumberDictionary[Rarity.Rare];
        float epic = epicPercentage + StageManager.Instance.rebirthCount * correctionNumberDictionary[Rarity.Unique];
        float legendary = legendaryPercentage + StageManager.Instance.rebirthCount * correctionNumberDictionary[Rarity.Legendary];

        this.rarity = rarity;
        nonePercentage = 100f - (normal + rare + epic + legendary);
        //Debug.Log(nonePercentage);
        this.normalPercentage = nonePercentage + normal;
        //Debug.Log(this.normalPercentage);
        this.rarePercentage = this.normalPercentage + rare;
        //Debug.Log(this.rarePercentage);
        this.epicPercentage = this.rarePercentage + epic;
        //Debug.Log($"{this.epicPercentage }            {epicPercentage}");
        this.legendaryPercentage = this.epicPercentage + legendary;
        //Debug.Log(this.legendaryPercentage);


        percentDictionary.Add(Rarity.Normal, this.normalPercentage);
        percentDictionary.Add(Rarity.Rare, this.rarePercentage);
        percentDictionary.Add(Rarity.Unique, this.epicPercentage);
        percentDictionary.Add(Rarity.Legendary, this.legendaryPercentage);

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

    //ȯ�� Ƚ��
    //ȯ�� ����ġ
    //�⺻ Ȯ�� ���̺�


    private ItemTable normalBox;
    private ItemTable rareBox;
    private ItemTable epicBox;
    private ItemTable legendaryBox;


    //���� = chestPercentageDictionary[rarity].percentDictionary[(int)i] i�� ���� �������� �븻���� ������������ Ȯ�� üũ�� �ִ°� ������?

    private Dictionary<Rarity, ItemTable> chestPercentageDictionary = new Dictionary<Rarity, ItemTable>();

    protected void Awake()
    {
        anim = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        canUse = false;
        canUse = true;
    }

    protected void OnEnable()
    {
        base.Start();
        normalBox = new ItemTable(Rarity.Normal, 50f, 20f, 0f, 0f);
        rareBox = new ItemTable(Rarity.Rare, 25f, 55f, 0f, 0f);
        epicBox = new ItemTable(Rarity.Unique, 5f, 15f, 70f, 1f);
        legendaryBox = new ItemTable(Rarity.Legendary, 20f, 40f, 25f, 15f);
        chestPercentageDictionary.Clear();
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
        //���⼭ ������ �޾ƿͼ� ���
        Item item = PoolManager.Instance.Pop("Item Temp") as Item;

        int randIdx = Random.Range(0, 100);

        if (StageManager.Instance.currentRoom.obstacles != null)
        {
            StageManager.Instance.currentRoom.obstacles.SetActive(false);
        }
        NeoRoomManager.instance.doorList.ForEach(door => { door.gameObject.SetActive(true); door.SetDoor(true); });
        item.transform.position = transform.position - new Vector3(.1f, 0, 0);
        item.transform.DOMove(transform.position - new Vector3(1, 1), 1f);
        item.canUse = true;



        print(randIdx);
        print(rarity);
        print(chestPercentageDictionary[rarity].GetPercent(Rarity.None));
        print(chestPercentageDictionary[rarity].GetPercent(Rarity.Normal));
        print(chestPercentageDictionary[rarity].GetPercent(Rarity.Rare));
        print(chestPercentageDictionary[rarity].GetPercent(Rarity.Unique));
        print(chestPercentageDictionary[rarity].GetPercent(Rarity.Legendary));

        if (randIdx < chestPercentageDictionary[rarity].GetPercent(Rarity.None)) 
        {
            //�� ��� X
            PoolManager.Instance.Push(item);
            print("No");
        }
        else if (chestPercentageDictionary[rarity].GetPercent(Rarity.None) <= randIdx && randIdx < chestPercentageDictionary[rarity].GetPercent(Rarity.Rare))
        {
            //�븻 ������ ���
            item.Init(Rarity.Normal);
            print("��");
        }
        else if (chestPercentageDictionary[rarity].percentDictionary[Rarity.Normal] <= randIdx && randIdx < chestPercentageDictionary[rarity].GetPercent(Rarity.Unique))
        {
            //���� ������ ���
            item.Init(Rarity.Rare);
            print("��");
        }
        else if (chestPercentageDictionary[rarity].GetPercent(Rarity.Rare) <= randIdx && randIdx < chestPercentageDictionary[rarity].GetPercent(Rarity.Legendary))
        {
            //����ũ ������ ���
            item.Init(Rarity.Unique);
            print("��");
        }
        else //if (chestPercentageDictionary[rarity].GetPercent(Rarity.Unique) <= randIdx && randIdx < chestPercentageDictionary[rarity].GetPercent(Rarity.Legendary))
        {
            //�������� ������ ���
            item.Init(Rarity.Legendary);
            print("���Գ�");
        }


       
        Invoke(nameof(PushChestInPool), .5f);
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

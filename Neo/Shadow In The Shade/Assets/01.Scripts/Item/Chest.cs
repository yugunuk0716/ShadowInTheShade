using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Rarity rarity;

    private BoxCollider2D boxCol;
    private Animator anim;
    private Rigidbody2D rigid;
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
        StageManager.Instance.onBattleEnd.AddListener(() => { });
        string[] a = transform.name.Split(' ');
        for (int i = 0; i < 3; i++)
        {
            if (a[0].Equals("Normal"))
            {
                rarity = Rarity.Normal;
            }
            else if (a[0].Equals("Rare"))
            {
                rarity = Rarity.Rare;
            }
            else if (a[0].Equals("Unnique"))
            {
                rarity = Rarity.Unique;
            }
            else if (a[0].Equals("Legendary"))
            {
                rarity = Rarity.Legendary;
            }
        }
    }

    public override void Use(GameObject target)
    {
        if (!canUse || used)
        {
            print("���� ����");
            return;
        }

        print("����");
        used = true;
        anim.SetTrigger("open");

        //���⼭ ������ �޾ƿͼ� ���
        targetItem = ItemManager.Instance.PickItem(rarity);
        Invoke(nameof(PushChestInPool), 3f);
    }


    private void PushChestInPool()
    {
        PoolManager.Instance.Push(this);
    }

    public void Popup(Vector3 pos)// �� �Լ��� �������� Ŭ����� Ǯ���� ���� ������ �����ϸ� ��
    {
        transform.position = pos;
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        StartCoroutine(OpenDelay());
    }

    IEnumerator OpenDelay()
    {
        rigid.gravityScale = 1f;
        yield return new WaitForSeconds(1f);
        rigid.gravityScale = 0f;
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

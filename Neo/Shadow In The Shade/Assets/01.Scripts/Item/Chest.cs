using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public Rarity rarity;

    private BoxCollider2D boxCol;
    private Animator anim;
    private Rigidbody2D rigid;

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
            print("���� ����");
            return; 
        }

        print("����");
        used = true;
        anim.SetTrigger("open");

        //���⼭ ������ �޾ƿͼ� ���
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

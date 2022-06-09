using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    ItemEasy,//�����۹� - ���� �⺻���� ��, ���� ���� ���� ����
    ItemNormal,//�����۹� - ���� �⺻���� ��, ���� ���� ������
    ItemHard,//�����۹� - ���� �⺻���� ��, ���� ���� ����
    Shop,//���� - ���� ��(�ѹ� �̿��ϸ� ����)
    Rebirth,//ȯ���� - �鰡�� �ٷ� ȯ��
    Chest,//���ڹ� - ���� ���� ���� ���� ���� �� �ִ� �� Ȥ�� �߰��������� �� ����
    Boss,//���� - ���� ������
}

public class NeoDoor : Interactable
{
    public NeoDoor pairDoor;
    public SpriteRenderer sr;

    public bool isOpened;

    private DoorSO curDoorData;

    private new void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StageManager.Instance.onBattleEnd.AddListener(() =>
        {
            isOpened = true;
            sr.sprite = curDoorData.openedDoor;
        });
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


    public void SetDoor(RoomType rt)
    {
        curDoorData = Resources.Load<DoorSO>($"Door/{rt}");
        sr.sprite = curDoorData.closedDoor;
    }

    public override void Use(GameObject target)
    {
        if (used || !isOpened)
        {
            print("���� ����");
            return;
        }
        SetDoor(NeoRoomManager.instance.LoadNextRoom());
        PoolManager.Instance.Push(this);
        if(pairDoor != null)
        {
            PoolManager.Instance.Push(pairDoor); 
        }
        used = true;
    }

    public override void Reset()
    {
        used = false;
    }
}

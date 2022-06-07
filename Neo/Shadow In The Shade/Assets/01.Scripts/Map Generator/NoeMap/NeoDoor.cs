using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Item,//�����۹� - ���� �⺻���� ��
    Shop,//���� - ���� ��(�ѹ� �̿��ϸ� ����)
    Rebirth,//ȯ���� - �鰡�� �ٷ� ȯ��
    Chest,//���ڹ� - ���� ���� ���� ���� ���� �� �ִ� �� Ȥ�� �߰��������� �� ����
    Boss,//���� - ���� ������
}

public class NeoDoor : Interactable
{
    public NeoDoor pairDoor;

    public RoomType roomType;

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
        switch (rt)
        {
            case RoomType.Item:
                print("������");
                break;
            case RoomType.Shop:
                print("��");
                break;
            case RoomType.Rebirth:
                print("ȯ��");
                break;
            case RoomType.Chest:
                print("����");
                break;
            case RoomType.Boss:
                print("��.��.");
                break;
        }
    }

    public override void Use(GameObject target)
    {
        if (used)
        {
            print("���� ����");
            return;
        }
        NeoRoomManager.instance.LoadNextRoom();
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

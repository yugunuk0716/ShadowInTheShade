using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    Item,//아이템방 - 가장 기본적인 방
    Shop,//상점 - 상점 방(한번 이용하면 닫힘)
    Rebirth,//환생방 - 들가면 바로 환생
    Chest,//상자방 - 도전 과제 깨면 상자 받을 수 있는 방 혹은 중간보스방이 될 예정
    Boss,//보스 - 가장 마지막
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
                print("아이템");
                break;
            case RoomType.Shop:
                print("샵");
                break;
            case RoomType.Rebirth:
                print("환생");
                break;
            case RoomType.Chest:
                print("상자");
                break;
            case RoomType.Boss:
                print("보.스.");
                break;
        }
    }

    public override void Use(GameObject target)
    {
        if (used)
        {
            print("오픈 실패");
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

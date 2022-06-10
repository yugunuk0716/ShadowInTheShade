using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    ItemEasy,//아이템방 - 가장 기본적인 방, 몹의 수가 가장 적음
    ItemNormal,//아이템방 - 가장 기본적인 방, 몹의 수가 적당함
    ItemHard,//아이템방 - 가장 기본적인 방, 몹의 수가 많음
    Shop,//상점 - 상점 방(한번 이용하면 닫힘)
    Rebirth,//환생방 - 들가면 바로 환생
    Chest,//상자방 - 도전 과제 깨면 상자 받을 수 있는 방 혹은 중간보스방이 될 예정
    Boss,//보스 - 가장 마지막
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
            print("오픈 실패");
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

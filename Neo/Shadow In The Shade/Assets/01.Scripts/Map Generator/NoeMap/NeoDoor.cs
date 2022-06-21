using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    ItemEasy,//아이템방 - 가장 기본적인 방, 몹의 수가 가장 적음
    ItemNormal,//아이템방 - 가장 기본적인 방, 몹의 수가 적당함
    ItemHard,//아이템방 - 가장 기본적인 방, 몹의 수가 많음
    Shop,//상점 - 상점 방(한번 이용하면 닫힘)
    Chest,//상자방 - 도전 과제 깨면 상자 받을 수 있는 방 혹은 중간보스방이 될 예정
    Boss,//보스 - 가장 마지막
    Rebirth,//환생방 - 들가면 바로 환생
    Turorial,//튜토리얼 때만 쓰는 방
}

public class NeoDoor : Interactable
{
    public NeoDoor pairDoor;
    public SpriteRenderer sr;

    private RoomType curRoomType;
    public bool isOpened;
    public bool isTutorial;

    private DoorSO curDoorData;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        StageManager.Instance.onBattleEnd.AddListener(() =>
        {
            isOpened = true;
            if (curDoorData != null) 
            {
                sr.sprite = curDoorData.openedDoor;
            }
        });

        GameManager.Instance.onPlayerTypeChanged.AddListener(() =>
        {
            if (PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates))
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.4f);
            }
            else
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
        });

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isOpened)
                return;
            base.OnTriggerEnter2D(collision);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }


    public void SetDoor(RoomType rt)
    {
        if (curDoorData == null)
        {
            curDoorData = Resources.Load<DoorSO>($"Door/{rt}");
        }
        sr.sprite = isOpened ? curDoorData.openedDoor : curDoorData.closedDoor;
        curRoomType = rt;

    }

    public void SetDoor(bool isClear)
    {
        isOpened = isClear;
        SetDoor(curRoomType);
    }

    public override void Use(GameObject target)
    {
        if (used || !isOpened)
        {
            print("오픈 실패");
            return;
        }

        UIManager.Instance.CloseInteractableGuideImage();


        if (NeoRoomManager.instance.doorList.Count > 0)
        {
            NeoRoomManager.instance.doorList.ForEach(door => PoolManager.Instance.Push(door));
        }
        else
        {
            PoolManager.Instance.Push(this);
            if (pairDoor != null)
            {
                PoolManager.Instance.Push(pairDoor);
            }
        }

        if (isTutorial)
        {
            NeoRoomManager.instance.LoadRoom("Tutorial");
        }
        else
        {
            NeoRoomManager.instance.LoadRoom(curRoomType);

        }

        StageManager.Instance.UseDoor();
        
     
        used = true;
    }

    public override void Reset()
    {
        used = false;
        transform.localScale = new Vector3(3.5f, 4f);
        pairDoor = null;
        curDoorData = null;
        isTutorial = false;
    }
}

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

    public RoomType curRoomType;
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
        if (!isOpened)
        {
            print("오픈 실패");
            return;
        }

        UIManager.Instance.CloseInteractableGuideImage();

        if (curRoomType.Equals(RoomType.Rebirth))
        {
            Rebirth();

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
            StageManager.Instance.UseDoor();
            return;
        }


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

    public void Rebirth()
    {
        //레벨 별로 레벨 깎고 스탯 분배 안한 상태로 돌리고 현재 레벨 만큼 스탯 포인트 다시 준다
        //레벨에 따라 상자 드랍

        NeoRoomManager.instance.LoadRoom("Start");
        NeoRoomManager.instance.experiencedRoomCount = 0;

        Rarity rarity = Rarity.Normal;
        switch (GameManager.Instance.playerSO.ectStats.LEV)
        {
            case 10:
            case 11:
            case 12:
                GameManager.Instance.playerSO.ectStats.LEV -= 9;
                rarity = SpawnItem(100, 0, 0, 0);
                break;
            case 13:
            case 14:
            case 15:
                GameManager.Instance.playerSO.ectStats.LEV -= 11;
                rarity = SpawnItem(60, 40, 0, 0);
                break;
            case 16:
            case 17:
            case 18:
            case 19:
                GameManager.Instance.playerSO.ectStats.LEV -= 12;
                rarity = SpawnItem(50, 47, 3, 0);
                break;
            case 20:
                GameManager.Instance.playerSO.ectStats.LEV -= 15;
                rarity = SpawnItem(10, 70, 5, 15);
                break;

        }

      
        if((int)rarity > 1) 
        {
            rarity = Rarity.Rare;
        }

        StageManager.Instance.rebirthCount++;
       
        Chest c = PoolManager.Instance.Pop($"{rarity} Chest") as Chest;
        c.Popup(StageManager.Instance.currentRoom.chestPointTrm.position);
        
    }


    private Rarity SpawnItem(int Normal, int rare, int epic, int legendary)
    {
        int idx = Random.Range(0, 100);
        Rarity result = Rarity.Normal;

        if (idx <= Normal)
        {
            result = Rarity.Normal;
        }
        else if (Normal < idx && idx <= Normal + rare)
        {
            result = Rarity.Rare;
        }
        else if (epic != 0 &&  Normal + rare < idx && idx < Normal + rare + epic)
        {
            result = Rarity.Unique;
        }
        else if(legendary != 0)
        {
            result = Rarity.Legendary;
        }

        return result;
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

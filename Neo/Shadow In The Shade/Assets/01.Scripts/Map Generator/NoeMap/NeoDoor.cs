using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    ItemEasy,//�����۹� - ���� �⺻���� ��, ���� ���� ���� ����
    ItemNormal,//�����۹� - ���� �⺻���� ��, ���� ���� ������
    ItemHard,//�����۹� - ���� �⺻���� ��, ���� ���� ����
    Shop,//���� - ���� ��(�ѹ� �̿��ϸ� ����)
    Chest,//���ڹ� - ���� ���� ���� ���� ���� �� �ִ� �� Ȥ�� �߰��������� �� ����
    Boss,//���� - ���� ������
    Rebirth,//ȯ���� - �鰡�� �ٷ� ȯ��
    Turorial,//Ʃ�丮�� ���� ���� ��
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
            print("���� ����");
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
        //���� ���� ���� ��� ���� �й� ���� ���·� ������ ���� ���� ��ŭ ���� ����Ʈ �ٽ� �ش�
        //������ ���� ���� ���

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

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
            print("���� ����");
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

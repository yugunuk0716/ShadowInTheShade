using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeoRoomManager : MonoBehaviour
{
    string currentStageName = "Dungeon";

    private readonly string[] currentStageNames = new string[] { "Dungeon" };

    public static NeoRoomManager instance;
    //public static NeoRoomManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            GameObject obj = new GameObject("NeoRoomManager");
    //            instance = obj.AddComponent<NeoRoomManager>();
    //            obj.GetComponent<NeoRoomManager>().spawnableRoomData = Resources.Load<RoomListSO>("1Stage Room List");
    //        }

    //        return instance;
    //    }
    //}

    public RoomListSO spawnableRoomData;

    private Dictionary<RoomType, List<Room>> spawnableRoomDataDictionary = new Dictionary<RoomType, List<Room>>();
    public List<NeoDoor> doorList = new List<NeoDoor>();

    public int stageIndex = 0;
    public int tutoIndex = 0;
    public const int tutoMaxIndex = 3;
    
    public int experiencedRoomCount = 0;
    private bool isExperiencedShop = false;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        List<Room> easyRoomList = new List<Room> ();
        List<Room> normalRoomList = new List<Room>();
        List<Room> hardRoomList = new List<Room>();

        easyRoomList = spawnableRoomData.roomList.FindAll(r => r.roomType == RoomType.ItemEasy);
        normalRoomList = spawnableRoomData.roomList.FindAll(r => r.roomType == RoomType.ItemNormal);
        hardRoomList = spawnableRoomData.roomList.FindAll(r => r.roomType == RoomType.ItemHard);

        spawnableRoomDataDictionary.Add(RoomType.ItemEasy, easyRoomList);
        spawnableRoomDataDictionary.Add(RoomType.ItemNormal, normalRoomList);
        spawnableRoomDataDictionary.Add(RoomType.ItemHard, hardRoomList);

        LoadRoom("Start");
    }

    public (int, int, int, int) CalcDoorTypes()
    {
        int first = Random.Range(0, 3);
        int second = Random.Range(0, 3);
        int third = Random.Range(0, 3);
        int fourth = 0;
        //if (!isExperiencedShop)
        if (false)
        {
            fourth = 3;
            if (experiencedRoomCount == 10)
            {
                //보스방
                //fiveth = 5;
            }
            else if (experiencedRoomCount == 5)
            {
                //상자방
                //fiveth = 4;
            }
        }
        else
        {
            if (experiencedRoomCount == 10)
            {
                //보스방
                fourth = 5;
            }
            else if (experiencedRoomCount == 5)
            {
                //상자방
                //fourth = 4;
            }
        }

        return (first, second, third, fourth);
    }


    public void SpawnDoor()
    {
       
        (int a, int b, int c, int d) = CalcDoorTypes();
        print($"{a}, {b}, {c}, {d}");
        NeoDoor nDoor = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
        nDoor.transform.position = StageManager.Instance.currentRoom.endPointTrm.position;
        nDoor.SetDoor((RoomType)a);
        doorList.Add(nDoor);
        nDoor.gameObject.SetActive(false);
        NeoDoor nDoor2 = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
        nDoor2.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.left * 2.5f;
        nDoor2.SetDoor((RoomType)b);
        doorList.Add(nDoor2);
        nDoor2.gameObject.SetActive(false);
        NeoDoor nDoor3 = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
        nDoor3.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.right * 2.5f;
        nDoor3.SetDoor((RoomType)c);
        doorList.Add(nDoor3);
        nDoor3.gameObject.SetActive(false);
        if (d != 0)
        {
            NeoDoor nDoor4 = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
            nDoor4.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.left * 5f;
            nDoor4.SetDoor((RoomType)d);
            doorList.Add(nDoor4);
            nDoor4.gameObject.SetActive(false);

            //if () //환생문 등장 조건 만족시
            {
                NeoDoor nDoor5 = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
                nDoor5.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.right * 5f;
                nDoor5.SetDoor(RoomType.Rebirth);
                doorList.Add(nDoor5);
                nDoor5.gameObject.SetActive(false);
            }
        }

        doorList.ForEach(d => d.SetDoor(false));

    }

    public void LoadRoom(RoomType rt)
    {
        switch (rt)
        {
            case RoomType.ItemEasy:
            case RoomType.ItemNormal:
            case RoomType.ItemHard:
                List<Room> roomList = spawnableRoomDataDictionary[rt];
                int idx = Random.Range(0, roomList.Count);
                LoadRoom(roomList[idx].name.Substring(currentStageName.Length + 1));
                break;
            case RoomType.Shop:
                LoadRoom("Shop");
                break;
            case RoomType.Chest:
                LoadRoom("Chest");
                break;
            case RoomType.Boss:
                LoadRoom("Boss");
                break;
           
            
        }
    }

    public void LoadRoom(string s)
    {
        if (StageManager.Instance.currentRoom != null && !s.Contains("Start"))
        {
            PoolManager.Instance.Push(StageManager.Instance.currentRoom);
            if (!s.Contains("Tutorial"))
            {
                experiencedRoomCount++;
                print(experiencedRoomCount);
                SpawnDoor();
            }
            
        }

        string roomName = $"{currentStageNames[stageIndex]} {s}";

        if (s.Contains("Tutorial"))
        {
            tutoIndex++;
            roomName = $"{currentStageNames[stageIndex]} {s} {tutoIndex}";
            if (tutoIndex > tutoMaxIndex)
            {
                roomName = $"{currentStageNames[stageIndex]} Start";
            }
        }

        Room room = PoolManager.Instance.Pop(roomName) as Room;
        room.transform.position = Vector3.zero;
        GameManager.Instance.player.position = room.spawnPointTrm.position;
        UIManager.Instance.StartFadeOut();
        StageManager.Instance.currentRoom = room;
        StageManager.Instance.EnterRoom();
        
        AstarPath.active.Scan();
    }


    public void BossRoomClear() 
    {
        stageIndex++;
        isExperiencedShop = false;
    }

  

    public void LoadShop()
    {
        isExperiencedShop = true;
    }




}

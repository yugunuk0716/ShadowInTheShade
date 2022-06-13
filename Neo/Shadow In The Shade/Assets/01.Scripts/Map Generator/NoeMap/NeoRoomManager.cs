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

    public int stageIndex = 0;
    
    private int experiencedRoomCount = 0;
    private bool isExperiencedShop = false;

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        LoadNextRoom("Start");
    }

    public RoomType LoadNextRoom(string s)
    {
        if (StageManager.Instance.currentRoom != null && !s.Contains("Start"))
        {
            PoolManager.Instance.Push(StageManager.Instance.currentRoom);
            experiencedRoomCount++;
            
        }
       

        Room room = PoolManager.Instance.Pop($"{currentStageNames[stageIndex]} {s}") as Room;
        //room.gameObject.SetActive(false);
        room.transform.position = Vector3.zero;
        GameManager.Instance.player.position = room.spawnPointTrm.position;
        UIManager.Instance.StartFadeOut();
        AstarPath.active.Scan();
        StageManager.Instance.currentRoom = room;
        StageManager.Instance.EnterRoom();
        
        return room.roomType;
      
    }

    public void BossRoomClear() 
    {
        stageIndex++;
        isExperiencedShop = false;
    }

    public void SpawnDoor(RoomType rt)
    {
        NeoDoor nDoor = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
        nDoor.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.left * 2;
        nDoor.SetDoor(rt);
        NeoDoor nDoor2 = PoolManager.Instance.Pop("Maybe Door") as NeoDoor;
        nDoor2.transform.position = StageManager.Instance.currentRoom.endPointTrm.position + Vector3.right * 2;
        nDoor2.SetDoor(rt);
        if (!isExperiencedShop) 
        {
            //맨 위에서 상점방 넣고

            if(experiencedRoomCount > 10)
            {
                //여기서 보스방 확률로 넣고
            }
            else if(experiencedRoomCount > 5)
            {
                //여기서 상자방 확률로 넣읍시다. 
            }


        }
        nDoor.pairDoor = nDoor2;
        nDoor2.pairDoor = nDoor;
    }

    public RoomType LoadNextRoom()
    {
        int idx = Random.Range(0, spawnableRoomData.roomList.Count);
        RoomType rt = LoadNextRoom(spawnableRoomData.roomList[idx].name.Substring(currentStageName.Length + 1));
        SpawnDoor(rt);
        return rt;
    }

    public void LoadShop()
    {
        isExperiencedShop = true;
        LoadNextRoom("Shop");
    }




}

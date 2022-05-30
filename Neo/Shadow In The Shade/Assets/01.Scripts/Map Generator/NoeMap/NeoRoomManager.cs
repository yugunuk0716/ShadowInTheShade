using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NeoRoomManager : MonoBehaviour
{
    string currentStageName = "Dungeon";
    
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

    private void Awake()
    {
        
        instance = this;
    }


    private void Start()
    {
        LoadNextRoom("Start");
    }

    public void LoadNextRoom(string s)
    {
        if (StageManager.Instance.currentRoom != null && !s.Contains("Start"))
        {
            PoolManager.Instance.Push(StageManager.Instance.currentRoom);
        }

        
        Room room = PoolManager.Instance.Pop($"{currentStageName} {s}") as Room;
        room.transform.position = Vector3.zero;
        GameManager.Instance.player.position = room.spawnPointTrm.position;
        UIManager.Instance.StartFadeOut();
        StageManager.Instance.currentRoom = room;
        StageManager.Instance.EnterRoom();
        
      
    }

    public void LoadNextRoom()
    {
        int idx = Random.Range(0, spawnableRoomData.roomList.Count);
        LoadNextRoom(spawnableRoomData.roomList[idx].name.Substring(currentStageName.Length + 1));
    }





}

using System.Collections;
using System.Collections.Generic;
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
        

    }


    private void Start()
    {
        LoadNextRoom("Start");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            int idx = Random.Range(0, spawnableRoomData.roomList.Count);
            LoadNextRoom(spawnableRoomData.roomList[idx].name.Substring(currentStageName.Length + 1));
        }
    }

    public void LoadNextRoom(string s)
    {
        if (StageManager.Instance.currentRoom != null)
        {
            PoolManager.Instance.Push(StageManager.Instance.currentRoom);
        }

        
        Room room = PoolManager.Instance.Pop($"{currentStageName} {s}") as Room;

        StageManager.Instance.currentRoom = room;
    }
    



}

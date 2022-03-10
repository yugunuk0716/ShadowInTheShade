using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private static RoomManager instance;
    public static RoomManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("RoomManager");
                obj.AddComponent<RoomManager>();
                instance = obj.GetComponent<RoomManager>();
            }

            return instance;
        }
    }


    string currentStageName = "Dungeon";

    RoomDataSO roomData;

    Queue<RoomDataSO> loadedRoomQueue = new Queue<RoomDataSO>();

    public List<Room> loadRooms = new List<Room>();

    private void Awake()
    {
        //이거 임시
        instance = this;
    }

    public bool IsRoomExited(int x, int y)
    {
        return loadRooms.Find(room => room.X == x && room.Y == y) != null;
    }


}

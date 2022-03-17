using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomManager : MonoBehaviour
{
    string currentWorldName = "Dungeon";

    private static RoomManager instance;
    public static RoomManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("RoomManager");
                obj.AddComponent<RoomManager>();
                obj.AddComponent<RoomGenerator>();
                obj.GetComponent<RoomGenerator>().dungeonGenerationData = Resources.Load<RoomGenerationData>("DungeonGenerationData");
                instance = obj.GetComponent<RoomManager>();
            }

            return instance;
        }
    }


    RoomInfo currentLoadRoomData;


    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;


    private void Update()
    {
        UpdateRoomQueue();
    }
    //private void Awake()
    //{
    //    Init();
    //}
    //public void Init()
    //{
    //    while (true)
    //    {
    //        print("들어옴");
    //        if (isLoadingRoom)
    //            break;
    //        if (loadRoomQueue.Count == 0)
    //        {
    //            print("큐가 0개임");
    //            if (!spawnedBossRoom && loadedRooms.Count > 0)
    //            {
    //                print("보스 스폰");
    //                StartCoroutine(SpawnBossRoom());
    //            }
    //            else if (spawnedBossRoom && !updatedRooms)
    //            {
    //                foreach (Room room in loadedRooms)
    //                {
    //                    room.RemoveUnconnectedDoors();
    //                }
    //                foreach (Room room in loadedRooms)
    //                {
    //                    room.ConnectRoom();
    //                }
    //                updatedRooms = true;
    //            }
    //            return;
    //        }

    //        currentLoadRoomData = loadRoomQueue.Dequeue();
    //        isLoadingRoom = true;
    //        LoadInResourcesRoom(currentLoadRoomData);
    //    }
    //}

    public void UpdateRoomQueue()
    {
        if (isLoadingRoom)
            return;


        if (loadRoomQueue.Count == 0)
        {
            print("큐가 0임");
            if (!spawnedBossRoom && loadedRooms.Count > 0)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                foreach (Room room in loadedRooms)
                {
                    room.ConnectRoom();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        print(isLoadingRoom);
        LoadInResourcesRoom(currentLoadRoomData);
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;

        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            print(loadedRooms.Count - 1);

            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = PoolManager.Instance.Pop($"{currentWorldName} End") as Room;
            tempRoom.X = bossRoom.X;
            tempRoom.Y = bossRoom.Y;
            bossRoom.gameObject.SetActive(false);
            //PoolManager.Instance.Push(bossRoom);
            //Room roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            //loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExited(x, y))
        {
            print("방이 이미 존재함");
            return;
        }

        RoomInfo roomInfo = new RoomInfo();
        roomInfo.name = name;
        roomInfo.X = x;
        roomInfo.Y = y;

        loadRoomQueue.Enqueue(roomInfo);
    }

    public void LoadInResourcesRoom(RoomInfo info)
    {
        Room room = PoolManager.Instance.Pop($"{currentWorldName} {info.name}") as Room;
        
    }



    public void RegisterRoom(Room room)
    {
        if (currentLoadRoomData == null)
        {
            return;
        }
        room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height);
        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;
        room.name = $"{currentLoadRoomData} - {currentLoadRoomData.name} {room.X} {room.Y}";
        room.transform.parent = transform;

        isLoadingRoom = false;

        loadedRooms.Add(room);

    }


    public bool DoesRoomExited(int x, int y)
    {
        return loadedRooms.Find(room => room.X == x && room.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[] {
            "Empty"
            //"Basic1"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

  

    

}

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

    string currentWorldName = "Dungeon";

    RoomInfo currentLoadRoomData;

    Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("Empty", 1, 0);
        LoadRoom("Empty", -1, 0);
        LoadRoom("Empty", 0, 1);
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    public void UpdateRoomQueue()
    {
        if (isLoadingRoom)
            return;

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        //StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        LoadInResourcesRoom(currentLoadRoomData);
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
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

        print($"{roomInfo.X} {roomInfo.Y} {roomInfo.name}");
        loadRoomQueue.Enqueue(roomInfo);
    }

    public void LoadInResourcesRoom(RoomInfo info)
    {
        print($"{currentWorldName} {info.name}");
        Room room = PoolManager.Instance.Pop($"{currentWorldName} {info.name}") as Room;
        
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = $"{currentWorldName} {info.name}";

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);
        while (!loadRoom.isDone)
        {
            yield return null;
        }


    }

    public void RegisterRoom(Room room)
    {
        if (currentLoadRoomData == null)
        {
            print("방 데이터 빔");
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

    //public void OnPlayerEnterRoom(Room room)
    //{
    //    CameraController.instance.currRoom = room;
    //    currRoom = room;

    //    StartCoroutine(RoomCoroutine());
    //}

    

}

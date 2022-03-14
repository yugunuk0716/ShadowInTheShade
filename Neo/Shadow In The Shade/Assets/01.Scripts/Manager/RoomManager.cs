using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    private void Awake()
    {
        //¿Ã∞≈ ¿”Ω√
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
        if (isLoadingRoom || loadRoomQueue.Count  == 0)
            return;

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;
        //StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
        LoadInResourcesRoom(currentLoadRoomData);
    }


    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExited(x,y))
        {
            print("¡∏¿Á");
            return;
        }

        RoomInfo roomInfo = new RoomInfo();
        roomInfo.Name = name;
        roomInfo.X = x;
        roomInfo.Y = y;

        loadRoomQueue.Enqueue(roomInfo);
    }

    public void LoadInResourcesRoom(RoomInfo info)
    {
        Room room = PoolManager.Instance.Pop($"{currentWorldName} {info.Name}") as Room;//Resources.Load<Room>($"{currentWorldName} {info.Name}");
        //Instantiate(room);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = $"{currentWorldName} {info.Name}";

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
            print("ƒø∑ª∆Æ ∑Î ∫ˆ");       
            return;
        }
        room.transform.position = new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height);
        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;
        room.name = $"{currentLoadRoomData} - {currentLoadRoomData.Name} {room.X} {room.Y}";
        room.transform.parent = transform;

        isLoadingRoom = false;

        loadedRooms.Add(room);

    }


    public bool DoesRoomExited(int x, int y)
    {
        return loadedRooms.Find(room => room.X == x && room.Y == y) != null;
    }


}

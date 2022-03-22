using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
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
    readonly Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    private Vector2Int pastArea;
    //readonly L<Vector2Int> positionHash = new HashSet<Vector2Int>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    public bool isMoving = false;

    public UnityEvent OnMoveRoomEvent;


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
        LoadInResourcesRoom(currentLoadRoomData);
    }

    IEnumerator SpawnBossRoom()
    {
        #region annotation
        //spawnedBossRoom = true;
        //yield return new WaitForSeconds(0.5f);
        //if (loadRoomQueue.Count == 0)
        //{
        //    Room bossRoom = loadedRooms[loadedRooms.Count - 1];
        //    print(bossRoom.name);
        //    GameObject newObj = new GameObject();
        //    newObj.AddComponent<Room>();
        //    Room tempRoom = newObj.GetComponent<Room>();
        //    tempRoom.X = bossRoom.X;
        //    tempRoom.Y = bossRoom.Y;
        //    print($"{tempRoom.X} {tempRoom.Y}");
        //    Destroy(bossRoom.gameObject);
        //    Destroy(newObj);
        //    var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
        //    loadedRooms.Remove(roomToRemove);
        //    LoadRoom("End", tempRoom.X, tempRoom.Y);
        //    isLoadingRoom = false;

        //    new WaitForSeconds(1f);

        //}
        #endregion
        spawnedBossRoom = true;

        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {

            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            GameObject newObj = new GameObject();
            newObj.AddComponent<Room>();
            Room tempRoom = newObj.GetComponent<Room>();
            tempRoom.X = bossRoom.X;
            tempRoom.Y = bossRoom.Y;
            Destroy(newObj);
            bossRoom.name = "Dungeon Empty";
            PoolManager.Instance.Push(bossRoom);
            Room roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            //PoolManager.Instance.Push(roomToRemove);
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

        if(loadRoomQueue.Count <= 0)
        {
            //List<Room> list = (List<Room>)loadedRooms.OrderByDescending(x => x.X);
            //Room room = list[0];
            //List<Room> list = (List<Room>)loadedRooms.OrderByDescending(x => x.X);
            EffectManager.Instance.minimapCamObj.transform.position = room.transform.position + new Vector3(0f, 0f, -10f);
        }
    }

    public void LoadInResourcesRoom(RoomInfo info)
    {
        print($"{currentWorldName} {info.name}");
        Room room = PoolManager.Instance.Pop($"{currentWorldName} {info.name}") as Room;
        if (room.name.Contains("End"))
        {
            room.RemoveUnconnectedDoors();
        }
        else if (room.name.Equals($"{currentWorldName} Empty") )//|| room.name.Equals($"{currentWorldName} Basic1"))
        {
            print($"{room.name}");
            room.gameObject.SetActive(false);
            PoolManager.Instance.Push(room);
        }
        

    }



    public void RegisterRoom(Room room)
    {
        if (currentLoadRoomData == null)
        {
            return;
        }
        if (DoesRoomExited(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            isLoadingRoom = false;
            return;
        }

        if (currentLoadRoomData.name.Contains("Basic1"))
        {
            room.Width = 46;
            room.Height = 30;
        }

        //float w = 1f;
        //float h = 1f;

        //if(pastArea.x == 23)
        //{
        //    w = 1.5f;
        //}
        //else if (pastArea.y == 15)
        //{
        //    h = 1.5f;
        //}

        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;

        pastArea = new Vector2Int(room.Width, room.Height);

        room.transform.position = new Vector3(currentLoadRoomData.X * room.Width  , currentLoadRoomData.Y * room.Height );
        if (room.name.Contains("Start"))
        {
            EffectManager.Instance.SetCamBound(room.camBound);
         
        }
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

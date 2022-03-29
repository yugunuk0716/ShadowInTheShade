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

public enum DiagonalDir
{
    N = 0,
    NE,
    E,
    ES,
    S,
    SW,
    W,
    WN
}

public class RoomManager : MonoBehaviour
{
    readonly string currentWorldName = "Dungeon";

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
    readonly List<Room> adjacentRoomList = new List<Room>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    private int higherX;
    private int higherY;

    public bool isMoving = false;

    public UnityEvent OnMoveRoomEvent;

    private void Awake()
    {
        if(OnMoveRoomEvent == null)
        {
            OnMoveRoomEvent = new UnityEvent();
        }
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
            int x = bossRoom.X;
            int y = bossRoom.Y;
            Destroy(bossRoom.gameObject);
            //bossRoom.name = "Dungeon Empty";
            //PoolManager.Instance.Push(bossRoom);
            Room roomToRemove = loadedRooms.Single(r => r.X == x && r.Y == y);
            loadedRooms.Remove(roomToRemove);
            //PoolManager.Instance.Push(roomToRemove);
            LoadRoom("End", x, y);

        }
    }

    //private void CreateBigRoom(Room mainRoom)
    //{
    //    //int adjacentIdx = 0;
    //    print(mainRoom.name);
    //    Room topRoom = mainRoom.GetTop();
    //    Room bottomRoom = mainRoom.GetBottom();
    //    Room rightRoom = mainRoom.GetRight();
    //    Room leftRoom = mainRoom.GetLeft();
    //    if (topRoom != null)
    //    {
    //        adjacentRoomList.Add(topRoom);
    //    }

    //    if(bottomRoom != null)
    //    {
    //        adjacentRoomList.Add(mainRoom.GetBottom());
    //    }

    //    if(rightRoom != null)
    //    {
    //        adjacentRoomList.Add(mainRoom.GetRight());
    //    }

    //    if(leftRoom != null)
    //    {
    //        adjacentRoomList.Add(mainRoom.GetLeft());
    //    }

    //    #region 사선 체크
    //    //for (int i = 0; i < 4; i ++)
    //    //{
    //    //    Room r = adjacentRoomList[i];

    //    //    if(r == null)
    //    //    {
    //    //        continue;
    //    //    }

    //    //    if(i % 2 == 0)
    //    //    {
    //    //        Room tRoom = r.GetTop();
    //    //        Room bRoom = r.GetBottom();
    //    //        if (tRoom != null )//&& adjacentRoomDataDic[(DiagonalDir)i + 1] == null)
    //    //        {
    //    //            if (!adjacentRoomList.Contains(tRoom))
    //    //            {
    //    //                print("T");
    //    //                adjacentRoomList.Add(tRoom);
    //    //            }
    //    //            //adjacentIdx++;
    //    //        }

    //    //        if (bRoom != null)// && adjacentRoomDataDic[(DiagonalDir)i - 1] == null)
    //    //        {
    //    //            if (!adjacentRoomList.Contains(bRoom))
    //    //            {
    //    //                print("B");
    //    //                adjacentRoomList.Add(bRoom);
    //    //            }
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        Room rRoom = r.GetRight();
    //    //        Room lRoom = r.GetLeft();
    //    //        if (rRoom != null)// && adjacentRoomDataDic[(DiagonalDir)i + 1] == null)
    //    //        {
    //    //            if (!adjacentRoomList.Contains(rRoom))
    //    //            {
    //    //                print("R");
    //    //                adjacentRoomList.Add(rRoom);
    //    //            }
    //    //            //adjacentIdx++;
    //    //        }

    //    //        if (lRoom != null)// && adjacentRoomDataDic[(DiagonalDir)8] == null)
    //    //        {
    //    //            if (!adjacentRoomList.Contains(lRoom))
    //    //            {
    //    //                print("L");
    //    //                adjacentRoomList.Add(lRoom);
    //    //            }
    //    //            //adjacentIdx++;
    //    //        }
    //    //    }
    //    //}


    //    #endregion
    //    print(adjacentRoomList.Count);
    //    Room tempR = adjacentRoomList[Random.Range(0, adjacentRoomList.Count)];
    //    int basicIdx = 1;
    //    if (tempR.X > mainRoom.X)
    //    {
    //        basicIdx = 3;
    //    }
    //    else if (tempR.X < mainRoom.X )
    //    {
    //        basicIdx = 4;
    //    }
    //    else if (tempR.Y > mainRoom.Y)
    //    {
    //        basicIdx = 2;
    //    }
    //    else if(tempR.Y < mainRoom.Y)
    //    {
    //        basicIdx = 1;
    //    }
    //    basicIdx = 3;

    //    Destroy(mainRoom.gameObject);
    //    Room roomToRemove = loadedRooms.Single(r => r.X == mainRoom.X && r.Y == mainRoom.Y);
    //    loadedRooms.Remove(roomToRemove);
    //    Destroy(tempR.gameObject);
    //    roomToRemove = loadedRooms.Single(r => r.X == tempR.X && r.Y == tempR.Y);
    //    loadedRooms.Remove(roomToRemove);


    //    LoadRoom($"Basic{basicIdx}", mainRoom.X, mainRoom.Y);
    //}

    //IEnumerator SpawnOtherSizeRoom()
    //{
    //    //spawnedBossRoom = true;

    //    yield return new WaitForSeconds(0.5f);

    //    if (loadRoomQueue.Count == 0)
    //    {
    //        adjacentRoomList.Clear();

    //        int roomIdx = Random.Range(3, loadedRooms.Count - 3);

    //        print(roomIdx);
    //        Room mainRoom = loadedRooms[roomIdx];
    //        CreateBigRoom(mainRoom);
    //    }

    //}

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

#region 미니맵 중앙방에 중점을 두게하는 코드
        //if(loadRoomQueue.Count <= 1)
        //{
        //    //List<Room> list = (List<Room>)loadedRooms.OrderByDescending(x => x.X);
        //    //Room room = list[0];
        //    //list = (List<Room>)loadedRooms.OrderByDescending(x => x.Y);
        //    //room.Y = list[0].Y;
        //    Vector3 movePos = Vector3.zero;
        //    int xIdx = higherX / 2;
        //    int yIdx = higherY / 2;
        //    Room room = FindRoom(xIdx, yIdx);

        //    if (room != null)
        //    {
        //        movePos = new Vector3(room.X * room.Width, room.Y * room.Height, -10f);
        //    }
        //    else
        //    {
        //        movePos = new Vector3(xIdx * 23, yIdx * 15, -10f);
        //    }

        //    EffectManager.Instance.minimapCamObj.transform.position = movePos; //= new Vector3(room.X * room.Width, room.Y * room.Height);
        //    //EffectManager.Instance.minimapCamObj.transform.position = room.transform.position + new Vector3(0f, 0f, -10f);
        //}
        #endregion

    }

    public void LoadInResourcesRoom(RoomInfo info)
    {
        print($"{currentWorldName} {info.name}");
        Room room = PoolManager.Instance.Pop($"{currentWorldName} {info.name}") as Room;
        if (room.name.Contains("End"))
        {
            room.RemoveUnconnectedDoors();
            foreach (Room r in loadedRooms)
            {
                r.ConnectRoom();
            }
        }
        else if (room.name.Equals($"{currentWorldName} Empty") || room.name.Equals($"{currentWorldName} Basic1"))
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

        room.X = currentLoadRoomData.X;
        room.Y = currentLoadRoomData.Y;

        if (Mathf.Abs(room.X) > Mathf.Abs(higherX))
        {
            higherX = room.X;
        }

        if (Mathf.Abs(room.Y) > Mathf.Abs(higherY))
        {
            higherY = room.Y;
        }

        //if (!room.name.Contains("Basic"))
        //{
        //    room.transform.position =
        //    new Vector3(currentLoadRoomData.X * room.Width, currentLoadRoomData.Y * room.Height);
        //}'

        if (room.name.Contains("Start"))
        {
            EffectManager.Instance.SetCamBound(room.camBound);
        }

        room.transform.position = new Vector3(currentLoadRoomData.X * room.Width,  currentLoadRoomData.Y * room.Height, 0f);
        #region 주석
        ////생각 해보니까 이게 x가 0보다 크다면이랑 0보다 작다면을 나눠야 될 듯 혹은 Y가 양수인지 음수인지
        //if (room.name.Contains("Basic1"))
        //{
        //    print(room.transform.position);
        //    room.transform.position = new Vector3(currentLoadRoomData.X * room.Width , 7.5f + currentLoadRoomData.Y * room.Height, 0f);
        //}
        //if (room.name.Contains("Basic2"))
        //{
        //    print(room.transform.position);
        //    room.transform.position = new Vector3(-currentLoadRoomData.X * room.Width, -currentLoadRoomData.Y * room.Height/3, 0f);
        //}
        //if (room.name.Contains("Basic3"))
        //{
        //    print(room.transform.position);
        //    room.transform.position = new Vector3(11.5f + currentLoadRoomData.X * room.Width , currentLoadRoomData.Y * room.Height, 0f);
        //}
        //if (room.name.Contains("Basic4"))
        //{
        //    print(room.transform.position);
        //    room.transform.position = new Vector3(-currentLoadRoomData.X * room.Width / 2 - room.Width / 2, -currentLoadRoomData.Y * room.Height, 0f);
        //}
        #endregion


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

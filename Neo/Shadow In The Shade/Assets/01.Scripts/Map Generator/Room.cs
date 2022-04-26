using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : PoolableMono
{
    [field:SerializeField]
    public int Width { get; set; }
    [field:SerializeField]
    public int Height { get; set; }
    [field:SerializeField]
    public int X { get; set; } 
    [field:SerializeField]
    public int Y { get; set; }


    public int phaseCount = 0;

    public List<Door> doorList = new List<Door>();

    public Vector2 leftSpawnPoint;
    public Vector2 rightSpawnPoint;
    public Vector2 topSpawnPoint;
    public Vector2 bottomSpawnPoint;

    public Collider2D camBound;

    public GameObject miniPlayerSprite;

    public GameObject shadowMap;
    public GameObject defaultMap;

    public bool isClear = false;

    private Door leftDoor;
    private Door rightDoor;
    private Door topDoor;
    private Door bottomDoor;

    public List<EnemySpawnPoint> currentESPList;


    public Room(int width, int height)
    {
        Width = width;
        Height = height;
    }


    private void Start()
    {
        GameManager.Instance.onPlayerChangeType.AddListener(() => 
        {
            shadowMap.SetActive(PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
            defaultMap.SetActive(!PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
        });

    }

    private void Awake()
    {
    }

    private void OnEnable()
    {
        currentESPList = GetComponentsInChildren<EnemySpawnPoint>().ToList();
        
        RoomManager.Instance.RegisterRoom(this);
        Door[] doors = GetComponentsInChildren<Door>();
        foreach (Door door in doors)
        {
            if (doorList.Count > 3)
                break;
            doorList.Add(door);
            switch (door.doorType)
            {
                case DirType.Left:
                    leftDoor = door;
                    break;
                case DirType.Right:
                    rightDoor = door;
                    break;
                case DirType.Top:
                    topDoor = door;
                    break;
                case DirType.Bottom:
                    bottomDoor = door;
                    break;

            }

        }
       

    }



    public void EnterRoom()
    {
        SpawnEnemies();
        StageManager.Instance.ClearCheck();
    }

    public void SpawnEnemies()
    {
         
        foreach (EnemySpawnPoint esp in StageManager.Instance.CurEnemySPList)
        {
            if(!esp.isSpawned && esp.phaseCount == phaseCount)
            {
                esp.isSpawned = true;
                esp.StartSpawn();
            }
        }
    }

    public Vector2 GetSpawnPoint(DirType dir)
    {
        switch (dir)
        {
            case DirType.Left:
                return leftSpawnPoint;
            case DirType.Right:
                return rightSpawnPoint;
            case DirType.Top:
                return topSpawnPoint;
            case DirType.Bottom:
                return bottomSpawnPoint;
            default:
                break;
        }

        return Vector2.zero;
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doorList)
        {
            switch (door.doorType)
            {
                case DirType.Right:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DirType.Left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DirType.Top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DirType.Bottom:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void ConnectRoom()
    {
        foreach (Door door in doorList)
        {
            switch (door.doorType)
            {
                case DirType.Right:
                    Room adjacentRightRoom = GetRight();
                    if (adjacentRightRoom != null)
                        door.adjacentRoom = adjacentRightRoom;
                    break;
                case DirType.Left:
                    Room adjacentLeftRoom = GetLeft();
                    if (adjacentLeftRoom != null)
                        door.adjacentRoom = adjacentLeftRoom;
                    break;
                case DirType.Top:
                    Room adjacentTopRoom = GetTop();
                    if (adjacentTopRoom != null)
                        door.adjacentRoom = adjacentTopRoom;
                    break;
                case DirType.Bottom:
                    Room adjacentBottomRoom = GetBottom();
                    if (adjacentBottomRoom != null)
                        door.adjacentRoom = adjacentBottomRoom;
                    break;
            }
        }
    }


    public Room GetRight()
    {
        return RoomManager.Instance.FindRoom(X + 1, Y);
    }
    public Room GetLeft()
    {
        return RoomManager.Instance.FindRoom(X - 1, Y);
    }
    public Room GetTop()
    {
        return RoomManager.Instance.FindRoom(X, Y + 1);
    }
    public Room GetBottom()
    {
        return RoomManager.Instance.FindRoom(X, Y - 1);
    }


    public override void Reset()
    {
        //foreach (EnemySpawnPoint esp in StageManager.Instance.CurEnemySPList)
        //{
        //    esp.isSpawned = false;

        //}
        //StageManager.Instance.curEnemySPList.Clear();
        phaseCount = 0;

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }
#endif
}
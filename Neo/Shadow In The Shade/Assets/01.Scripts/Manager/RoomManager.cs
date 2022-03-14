using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    private const int MIN_MAP_SIZE_INCLUSIVE = 1;
    [SerializeField]
    private const int MAX_MAP_SIZE_EXCLUSIVE = 3;

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

    public int maxMapCount = 5;
    public int currentMapCount = 0;

    public Room root;

    private readonly Hashtable roomTable = new Hashtable();


    public void AddRoom(Vector2Int key, Room val)
    {
        roomTable.Add(key, val);
    }

    public bool CheckRoom(Vector2Int key)
    {
        return roomTable.Contains(key);
    }

    public Room FindRooom(Vector2Int key)
    {
        if (CheckRoom(key))
        {
            return roomTable[key] as Room;
        }

        return null;
    }

    public void GenerateMap()
    {
        int doorCount = Random.Range(0, maxMapCount / currentMapCount % 3);
        //여기서 전체 맵 Data를 만듬
        for (int i = 0; i < maxMapCount; i++)
        {
            int x = Random.Range(MIN_MAP_SIZE_INCLUSIVE, MAX_MAP_SIZE_EXCLUSIVE);
            int y = Random.Range(MIN_MAP_SIZE_INCLUSIVE, MAX_MAP_SIZE_EXCLUSIVE);

            if (root == null)
            {
                root = new Room(x, y);
                AddRoom(Vector2Int.zero, root);
                continue;
            }

            //if()
            
        }
    }

    public void DrawMap()
    {
        // 이동시 이동 한 맵을 그림

    }

    public void EraseMap()
    {
        // 이동시 원래 있던 맵을 지움

    }

}

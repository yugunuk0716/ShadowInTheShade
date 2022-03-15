using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainSpace
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField]
        private const int MIN_MAP_SIZE_INCLUSIVE = 1;

        [SerializeField]
        private const int MAX_MAP_SIZE_EXCLUSIVE = 3;

        [SerializeField]
        private const int ACCUMULATION = 20;

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

        private Room root;
        private Room roomPrefab;
        private Room currentRoom;

        private readonly Hashtable roomTable = new Hashtable();

        private Vector2Int playerPos = Vector2Int.zero;

        public void AddRoom(Vector2Int key, Room val)
        {
            roomTable.Add(key, val);
        }

        public bool CheckRoom(Vector2Int key)
        {
            return roomTable.Contains(key);
        }

        public Room FindRoom(Vector2Int key)
        {
            if (CheckRoom(key))
            {
                return roomTable[key] as Room;
            }

            return null;
        }

        public void GenerateRoom()
        {
            //여기서 전체 맵 Data를 만듬
            //int doorCount = Random.Range(0, maxMapCount % 3);

            int width = Random.Range(MIN_MAP_SIZE_INCLUSIVE, MAX_MAP_SIZE_EXCLUSIVE);
            int height = Random.Range(MIN_MAP_SIZE_INCLUSIVE, MAX_MAP_SIZE_EXCLUSIVE);
            print($"{width * ACCUMULATION} x {height * ACCUMULATION}");
            roomPrefab = PoolManager.Instance.Pop($"{width * ACCUMULATION} x {height * ACCUMULATION}") as Room;
            roomPrefab.gameObject.SetActive(false);

            if (root == null)
            {
                root = roomPrefab;
                root.Width = width;
                root.Height = height;

                AddRoom(Vector2Int.zero, root);
            }
            //여기서 랜덤생성 


        }

        public void DrawMap()
        {
            // 이동시 이동 한 맵을 그림
            currentRoom = FindRoom(playerPos);
            if (currentRoom != null)
            {


                currentRoom.gameObject.SetActive(true);
            }

        }

        public void EraseMap()
        {
            // 이동시 원래 있던 맵을 지움

        }

    }
}

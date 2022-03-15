using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public RoomGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = RoomCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomManager.Instance.LoadRoom("Start", 0, 0);
        foreach (Vector2Int roomLocation in rooms)
        {
            RoomManager.Instance.LoadRoom(RoomManager.Instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
        }
    }
}

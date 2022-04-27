using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
};

public class RoomCrawlerController : MonoBehaviour
{

    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}
    };

    public static List<Vector2Int> GenerateDungeon(RoomGenerationData dungeonData)
    {
        List<RoomCrawler> dungeonCrawlers = new List<RoomCrawler>();

        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new RoomCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        for (int i = 0; i < iterations; i++)
        {
            for (int j = 0; j < dungeonCrawlers.Count; j++)
            {
                Vector2Int newPos = dungeonCrawlers[j].Move(directionMovementMap);

                if(positionsVisited.Contains(newPos))
                {
                    j--;
                    continue;
                }

                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
    }


}

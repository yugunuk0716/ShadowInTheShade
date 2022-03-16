using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DirType
{
    Left,
    Right,
    Top,
    Bottom,
}

public class Door : MonoBehaviour
{
    public Room adjacentRoom;

    public DirType doorType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (adjacentRoom == null)
                return;
            print(adjacentRoom.GetSpawnPoint(doorType));
            collision.transform.SetParent(adjacentRoom.transform);
            collision.transform.localPosition = adjacentRoom.GetSpawnPoint(doorType);
            print(collision.transform.localPosition);
        }
    }
}

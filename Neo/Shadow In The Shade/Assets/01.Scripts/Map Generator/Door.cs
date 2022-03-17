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
            StartCoroutine(MoveRoomCoroutine(collision));
            
        }
    }

    IEnumerator MoveRoomCoroutine(Collider2D collision)
    {
        if (adjacentRoom == null)
            yield break;
        GameManager.Instance.timeScale = 0f;
        print(adjacentRoom.GetSpawnPoint(doorType));
        collision.transform.SetParent(adjacentRoom.transform);
        collision.transform.localPosition = adjacentRoom.GetSpawnPoint(doorType);
        print(collision.transform.localPosition);
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.timeScale = 1f;

    }
}

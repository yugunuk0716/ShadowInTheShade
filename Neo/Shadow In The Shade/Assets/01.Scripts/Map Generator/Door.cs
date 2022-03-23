using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        if (collision.CompareTag("Player") && !RoomManager.Instance.isMoving)
        {
            
            StartCoroutine(MoveRoomCoroutine(collision));
            
        }
    }
    
    IEnumerator MoveRoomCoroutine(Collider2D collision)
    {
        if (adjacentRoom == null)
            yield break;
        RoomManager.Instance.OnMoveRoomEvent?.Invoke();
        RoomManager.Instance.isMoving = true;
        GameManager.Instance.timeScale = 0f;
        print(adjacentRoom.GetSpawnPoint(doorType));
        collision.transform.SetParent(adjacentRoom.transform);
        PlayerMove agentMove = collision.GetComponent<PlayerMove>();
        Vector2 velocity = agentMove.rigid.velocity;
        agentMove.rigid.velocity = Vector2.zero;
        print(agentMove.rigid.velocity);
        collision.transform.DOLocalMove(adjacentRoom.GetSpawnPoint(doorType), 2f);
        EffectManager.Instance.minimapCamObj.transform.position = adjacentRoom.transform.position + new Vector3(0f, 0f, -10f);
        //Vector2 dir = adjacentRoom.GetSpawnPoint(doorType);
        //collision.GetComponent<PlayerMove>().OnMove(dir, 2f);
        EffectManager.Instance.SetCamBound(adjacentRoom.camBound);
        print(collision.transform.localPosition);
        yield return new WaitForSeconds(2f);
        agentMove.rigid.velocity = velocity;
        RoomManager.Instance.isMoving = false;
        GameManager.Instance.timeScale = 1f;

    }
}

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
    [HideInInspector]
    public Room adjacentRoom;

    public DirType doorType;
    public GameObject openedDoor;
    public GameObject closedDoor;


    private bool isOpen;
    public bool IsOpen
    {
        get
        {
            return isOpen;
        }
        set
        {
            isOpen = value;
            openedDoor.SetActive(isOpen);
            closedDoor.SetActive(!isOpen);
        }
    }

    private void Start()
    {
        IsOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !RoomManager.Instance.isMoving && isOpen)
        {
            collision.GetComponent<Player>().currentRoom = adjacentRoom;
            StartCoroutine(MoveRoomCoroutine(collision));
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }
    
    IEnumerator MoveRoomCoroutine(Collider2D collision)
    {
        if (adjacentRoom == null)
            yield break;

        Rigidbody2D rigd = collision.GetComponent<Rigidbody2D>();

        if (rigd.velocity.x > 10f || rigd.velocity.x < -10f || 
            rigd.velocity.y > 10f || rigd.velocity.x < -10f || 
            collision.GetComponent<PlayerDash>().isDash)
            yield break;

        RoomManager.Instance.isMoving = true;
        GameManager.Instance.timeScale = 0f;
        print(adjacentRoom.GetSpawnPoint(doorType));
        collision.transform.SetParent(adjacentRoom.transform);
        PlayerMove agentMove = collision.GetComponent<PlayerMove>();
        agentMove.rigid.velocity = Vector2.zero;
        SpriteRenderer playerSprite = collision.gameObject.GetComponentInChildren<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>();
        playerSprite.color = Color.black;
        playerSprite.color += new Color(0, 0, 0, -1f);

        //

        Vector2 offset = Vector2.zero;
        switch(doorType)
        {
            case DirType.Left:
                offset = new Vector2(.1f, 0);
                break;
            case DirType.Right:
                offset = new Vector2(-.1f, 0);
                break;
            case DirType.Top:
                offset = new Vector2(0,-.1f);
                break;
            case DirType.Bottom:
                offset = new Vector2(0, .1f);
                break;
        }

        collision.gameObject.transform.localPosition = adjacentRoom.GetSpawnPoint(doorType) + offset;
        Sequence playerMove = DOTween.Sequence();

        playerMove.Append(playerSprite.DOColor(Color.white, 1.5f)).OnComplete(() => { agentMove.rigid.velocity = Vector2.zero; collision.GetComponent<Rigidbody2D>().velocity = agentMove.rigid.velocity; });
        playerMove.Join(collision.transform.DOLocalMove(adjacentRoom.GetSpawnPoint(doorType), .5f));
        playerMove.Insert(1f, playerSprite.DOFade(1, 1f));

        EffectManager.Instance.minimapCamObj.transform.position = adjacentRoom.transform.position + new Vector3(0f, 0f, -10f);
        EffectManager.Instance.SetCamBound(adjacentRoom.camBound);
        yield return new WaitForSeconds(2f);
        RoomManager.Instance.OnMoveRoomEvent?.Invoke();
        RoomManager.Instance.isMoving = false;
        GameManager.Instance.timeScale = 1f;
    }
}

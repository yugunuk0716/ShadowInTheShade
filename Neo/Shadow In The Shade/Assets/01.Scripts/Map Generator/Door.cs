using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public enum DirType
{
    Left,
    Right,
    Top,
    Bottom,
    Boss
}

public class Door : MonoBehaviour
{
    [HideInInspector]
    public Room adjacentRoom;

    public DirType doorType;
    public GameObject openedDoor;
    public GameObject closedDoor;

    public GameObject shadowDoor;
    public GameObject normalDoor;
    

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
        //IsOpen = true;
        if(doorType == DirType.Boss)
        {
            IsOpen = true;
        }

        GameManager.Instance.onPlayerTypeChanged.AddListener(() =>
        {
            normalDoor.SetActive(!PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
        });
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !RoomManager.Instance.isMoving && isOpen)
        {
            StageManager.Instance.currentRoom.miniPlayerSprite.SetActive(false);
            if (!doorType.Equals(DirType.Boss))
            {
                //UIManager.Instance.StartFadeIn();
                StageManager.Instance.currentRoom = adjacentRoom;
                StageManager.Instance.currentRoom.miniPlayerSprite.SetActive(true);
                StageManager.Instance.CurEnemySPList.Clear();
                StageManager.Instance.currentRoom.currentESPList = StageManager.Instance.currentRoom.GetComponentsInChildren<EnemySpawnPoint>().ToList();
                StartCoroutine(MoveRoomCoroutine(collision));
            }
            else
            {
                Room bossRoom = RoomManager.Instance.loadedRooms.Find(r => r.name.Contains("Boss"));
                if(bossRoom != null)
                {
                    print("?");
                    StageManager.Instance.CurEnemySPList.Clear();
                    StageManager.Instance.currentRoom = bossRoom;
                    StartCoroutine(MoveBossRoomCoroutine(collision, bossRoom));
                }
                else
                {
                    print("보스룸이 없어용");
                }
            }
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }
    
    IEnumerator MoveRoomCoroutine(Collider2D collision)
    {
        if (adjacentRoom == null)
            yield break;
        UIManager.Instance.StartFadeOut();
        Rigidbody2D rigd = collision.GetComponent<Rigidbody2D>();

        if (rigd.velocity.x > 10f || rigd.velocity.x < -10f || 
            rigd.velocity.y > 10f || rigd.velocity.x < -10f || 
            collision.GetComponent<PlayerDash>().isDash)
            yield break;

        RoomManager.Instance.isMoving = true;
        GameManager.Instance.timeScale = 0f;
        collision.transform.SetParent(adjacentRoom.transform);
        PlayerMove agentMove = collision.GetComponent<PlayerMove>();
        agentMove.rigid.velocity = Vector2.zero;
        SpriteRenderer playerSprite = collision.gameObject.GetComponentInChildren<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>();
        playerSprite.color = Color.black;
        playerSprite.color += new Color(0, 0, 0, -1f);

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

        EffectManager.Instance.SetCamBound(adjacentRoom.camBound);
        playerMove.Append(playerSprite.DOColor(Color.white, 1.5f)).OnComplete(() => { agentMove.rigid.velocity = Vector2.zero; collision.GetComponent<Rigidbody2D>().velocity = agentMove.rigid.velocity; });
        playerMove.Join(collision.transform.DOLocalMove(adjacentRoom.GetSpawnPoint(doorType), .1f)).OnComplete(() => 
        {
            EffectManager.Instance.minimapCamObj.transform.position = adjacentRoom.transform.position + new Vector3(0f, 0f, -10f);
        });
        playerMove.Insert(1f, playerSprite.DOFade(1, 1f));

        
        yield return new WaitForSeconds(2f);
        RoomManager.Instance.OnMoveRoomEvent?.Invoke();
        RoomManager.Instance.isMoving = false;
        GameManager.Instance.timeScale = 1f;
        StageManager.Instance.currentRoom.EnterRoom();
    }

    IEnumerator MoveBossRoomCoroutine(Collider2D collision, Room bossRoom)
    {
        UIManager.Instance.StartFadeOut();
        Rigidbody2D rigd = collision.GetComponent<Rigidbody2D>();

        if (rigd.velocity.x > 10f || rigd.velocity.x < -10f ||
            rigd.velocity.y > 10f || rigd.velocity.x < -10f ||
            collision.GetComponent<PlayerDash>().isDash)
            yield break;

        RoomManager.Instance.isMoving = true;
        GameManager.Instance.timeScale = 0f;
        collision.transform.SetParent(bossRoom.transform);

        PlayerMove agentMove = collision.GetComponent<PlayerMove>();
        agentMove.rigid.velocity = Vector2.zero;
        SpriteRenderer playerSprite = collision.gameObject.GetComponentInChildren<PlayerAnimation>().gameObject.GetComponent<SpriteRenderer>();
        playerSprite.color = Color.black;
        playerSprite.color += new Color(0, 0, 0, -1f);

        collision.transform.position = bossRoom.transform.position;
        Sequence playerMove = DOTween.Sequence();
        
        EffectManager.Instance.SetCamBound(bossRoom.camBound);
        playerMove.Append(playerSprite.DOColor(Color.white, 1.5f)).OnComplete(() => { agentMove.rigid.velocity = Vector2.zero; collision.GetComponent<Rigidbody2D>().velocity = agentMove.rigid.velocity; });
        playerMove.Join(collision.transform.DOLocalMove(bossRoom.GetSpawnPoint(doorType), .1f)).OnComplete(() =>
        {
            EffectManager.Instance.minimapCamObj.transform.position = bossRoom.transform.position + new Vector3(0f, 0f, -10f);
        });
        playerMove.Insert(1f, playerSprite.DOFade(1, 1f));


        yield return new WaitForSeconds(2f);
        RoomManager.Instance.OnMoveRoomEvent?.Invoke();
        RoomManager.Instance.isMoving = false;
        GameManager.Instance.timeScale = 1f;
        //StageManager.Instance.currentRoom.EnterRoom();
        bossRoom.SpawnEnemies();
        DOTween.To(() => UIManager.Instance.bossHPBarCG.alpha, value => UIManager.Instance.bossHPBarCG.alpha = value, 1, 0.8f);
    }
}

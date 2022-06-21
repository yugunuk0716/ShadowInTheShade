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


    public RoomType roomType;

    public int phaseCount = 0;

    public List<Door> doorList = new List<Door>();

    public Vector2 leftSpawnPoint;
    public Vector2 rightSpawnPoint;
    public Vector2 topSpawnPoint;
    public Vector2 bottomSpawnPoint;

    public Transform spawnPointTrm;
    public Transform endPointTrm;
    public Transform chestPointTrm;

    public Collider2D camBound;

    public GameObject miniPlayerSprite;
    public GameObject obstacles;

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
        GameManager.Instance.onPlayerTypeChanged.AddListener(() => 
        {
            shadowMap.SetActive(PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
            defaultMap.SetActive(!PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
        });

    }

    //private void Awake()
    //{
    //    spawnPointTrm.position.Set(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
    //    spawnPointTrm.position.Set(Random.Range(0f, 1f), Random.Range(0f, 1f), 0);
    //}

    private void OnEnable()
    {
        currentESPList = GetComponentsInChildren<EnemySpawnPoint>().ToList();
        
        RoomManager.Instance.RegisterRoom(this);
       
       

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
        UIManager.Instance.enemiesCountText.text = $"³²Àº Àû: {StageManager.Instance.curStageEnemys.Count}";
    }

    public override void Reset()
    {
        foreach (EnemySpawnPoint esp in currentESPList)
        {
            esp.isSpawned = false;

        }
        //StageManager.Instance.curEnemySPList.Clear();
        phaseCount = 0;
        isClear = false;
        if(obstacles != null)
        {
            obstacles.SetActive(true);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }
#endif
}

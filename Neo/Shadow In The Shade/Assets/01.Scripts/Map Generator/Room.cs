using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : PoolableMono
{

    public RoomType roomType;

    public int phaseCount = 0;

    public Transform spawnPointTrm;
    public Transform endPointTrm;
    public Transform chestPointTrm;

    public Collider2D camBound;

    public GameObject miniPlayerSprite;
    public GameObject obstacles;

    public GameObject shadowMap;
    public GameObject defaultMap;

    public bool isClear = false;

    public List<EnemySpawnPoint> currentESPList;


    private void Start()
    {
        GameManager.Instance.onPlayerTypeChanged.AddListener(() => 
        {
            shadowMap.SetActive(PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
            defaultMap.SetActive(!PlayerStates.Shadow.Equals(GameManager.Instance.playerSO.playerStates));
        });

    }

   

    private void OnEnable()
    {
        currentESPList = GetComponentsInChildren<EnemySpawnPoint>().ToList();
        
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
    }
#endif
}

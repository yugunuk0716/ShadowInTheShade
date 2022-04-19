using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("RoomManager");
                obj.AddComponent<StageManager>();
                instance = obj.GetComponent<StageManager>();
            }

            return instance;
        }
    }

    public Room currentRoom;

    public List<Enemy> curStageEnemys = new List<Enemy>();
    public List<EnemySpawnPoint> CurEnemySPList
    {
        get
        {
            if (curEnemySPList.Count == 0)
                curEnemySPList = currentRoom.currentESPList;
            return curEnemySPList;
        }
        set
        {
            CurEnemySPList = value;
        }
    }
    private List<EnemySpawnPoint> curEnemySPList = new List<EnemySpawnPoint>();

    private AudioClip doorAudioClip;

    private void Awake()
    {
        doorAudioClip = Resources.Load<AudioClip>("Sounds/DoorOpen");
    }

    private void Start()
    {
        RoomManager.Instance.OnMoveRoomEvent.AddListener(() =>
        {
            //currentRoom.isClear = false;
            currentRoom.doorList.ForEach(d =>
            {
                d.IsOpen = currentRoom.isClear;
                d.shadowDoor.SetActive(GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow) && currentRoom.isClear);
            });
            PlayDoorSound();
        });

        GameManager.Instance.onPlayerChangeType.AddListener(() =>
        {
            currentRoom.doorList.ForEach(d =>
            {
                d.shadowDoor.SetActive(GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow) && currentRoom.isClear);
            });
        });
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(CurEnemySPList.Count);
        }
    }

    public void ClearCheck()
    {
        if (curStageEnemys.Count > 0)
        {
            return;
        }

        if (CurEnemySPList.Find(esp => !esp.isSpawned) != null)
        {
            currentRoom.phaseCount++;
            currentRoom.SpawnEnemies();
            return;
        }
        StageClear();
    }


    public void StageClear()
    {
        currentRoom.isClear = true;
        currentRoom.doorList.ForEach(d =>
        {
            d.IsOpen = true;
            d.shadowDoor.SetActive(GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow) && currentRoom.isClear);
        });
        PlayDoorSound();
    }

    private void PlayDoorSound()
    {
        SoundManager.Instance.GetAudioSource(doorAudioClip, false, SoundManager.Instance.BaseVolume).Play();
    }
}

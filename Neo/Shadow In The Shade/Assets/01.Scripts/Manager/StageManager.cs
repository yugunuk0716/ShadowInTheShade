using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

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

    public Light2D globalLight;
    public Room currentRoom;
    public bool isBattle = false;
    public UnityEvent onBattleEnd = new UnityEvent();

    private Color shadowColor = new Color(60 / 255f, 60 / 255f, 60 / 255f);

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
        globalLight = GameObject.Find("Global Light").GetComponent<Light2D>();
    }

    private void Start()
    {
        RoomManager.Instance.OnMoveRoomEvent.AddListener(() =>
        {
            //currentRoom.isClear = false;
            if (!currentRoom.isClear)
            {
                isBattle = true;
                globalLight.intensity = 0.45f;
                globalLight.color = shadowColor;
                DOTween.To(() => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize, f => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize = f, 6f, 1f);
            }
            currentRoom.doorList.ForEach(d =>
            {
                d.IsOpen = currentRoom.isClear;
                d.shadowDoor.SetActive(GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow) && currentRoom.isClear);
            });
            PlayDoorSound();
        });


        GameManager.Instance.onPlayerChangeType.AddListener(() =>
        {
            if (currentRoom != null)
            {
                currentRoom.doorList.ForEach(d =>
                {
                    d.shadowDoor.SetActive(GameManager.Instance.playerSO.playerStates.Equals(PlayerStates.Shadow) && currentRoom.isClear);
                });
            }
            else
            {
                print("현재 방이 없음");
            }
        });


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
        if (isBattle)
        {
            onBattleEnd?.Invoke();
            Chest c = PoolManager.Instance.Pop("Normal Chest") as Chest;
            c.Popup(currentRoom.transform.position);
        }
        isBattle = false;
        globalLight.intensity = 1f;
        globalLight.color = shadowColor * 2;
        DOTween.To(() => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize, f => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize = f, 7.5f, 1f);
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

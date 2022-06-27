using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
using System.Linq;

public class StageManager : MonoBehaviour
{
    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("StageManager");
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

    public int rebirthCount = 2;

    private Chest chest;

    private Color shadowColor = new Color(60 / 255f, 60 / 255f, 60 / 255f);

    public List<OldEnemy> curStageEnemys = new List<OldEnemy>();
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

    public void UseDoor()
    {
        if (!currentRoom.isClear)
        {
            isBattle = true;
            globalLight.intensity = 0.45f;
            globalLight.color = shadowColor;
            DOTween.To(() => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize, f => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize = f, 6f, 1f);
        }

        PlayDoorSound();
    }

    public void EnterRoom()
    {
        //print("���ͷ�");
        if(chest != null)
        {
            PoolManager.Instance.Push(chest);
        }
        CurEnemySPList.Clear();
        EffectManager.Instance.SetCamBound(currentRoom.camBound);
        currentRoom.currentESPList = currentRoom.GetComponentsInChildren<EnemySpawnPoint>().ToList();
        currentRoom.EnterRoom();
        if (!currentRoom.isClear)
        {
            isBattle = true;
            globalLight.intensity = 0.45f;
            globalLight.color = shadowColor;
            DOTween.To(() => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize, f => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize = f, 6f, 1f);
        }

       
    }

    public void ClearCheck()
    {
        UIManager.Instance.enemiesCountText.text = $"���� ��: {curStageEnemys.Count}";
        if (curStageEnemys.Count > 0)
        {
            return;
        }

        if (CurEnemySPList.Find(esp => !esp.isSpawned) != null)
        {
            currentRoom.phaseCount++;
            if (currentRoom.phaseCount >= 2 && NeoRoomManager.instance.experiencedRoomCount <= 2)
            {
                curStageEnemys.Clear();
                StageClear();
                return;
            }
            currentRoom.SpawnEnemies();
            return;
           
        }
        StageClear();
    }


    public void StageClear()
    {
        currentRoom.isClear = true;
        if (isBattle && (!currentRoom.name.Contains("Boss") || !currentRoom.name.Contains("End") || !currentRoom.name.Contains("Start")))
        {
            onBattleEnd?.Invoke();
            Rarity rarity = Rarity.Normal;
            int idx = Random.Range(0, 100);
            bool canDrop = true;

            idx += 50;
            if(idx < 20)
            {
                canDrop = false; 
            }
            else if(19 < idx && idx < 40 )
            {
                rarity = Rarity.Normal;  
            }
            else if( 39 < idx && idx < 60)
            {
                rarity = Rarity.Rare;
            }
         /*   else if (97 < idx && idx < 80)
            {
                rarity = Rarity.Unique;
            }
            else
            {
                rarity = Rarity.Legendary;
            }*/
           

            if (canDrop)
            {
                chest = PoolManager.Instance.Pop($"{rarity} Chest") as Chest;
                chest.Popup(currentRoom.chestPointTrm.position);
            }
        }
        isBattle = false;
        globalLight.intensity = 1f;
        globalLight.color = shadowColor * 2;
        DOTween.To(() => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize, f => EffectManager.Instance.cinemachineCamObj.m_Lens.OrthographicSize = f, 7.5f, 1f);
      

       
        
        PlayDoorSound();
    }

    private void PlayDoorSound()
    {
        SoundManager.Instance.GetAudioSource(doorAudioClip, false, SoundManager.Instance.BaseVolume).Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StageManager : MonoSingleton<StageManager>
{
    public List<Room> _rooms;
    public bool _isTutorial = false;
    public bool _isClear = false;
    public Light2D globalLight;
    public Room _currentRoom;





    public Color _shadowLightColor;
    public Color _normalLightColor;
    //public GameObject _tutoEntry;
    //public GameObject _stageEntry;

    private void Awake()
    {
        _rooms = new List<Room>();
    }

    private void Start()
    {

        //if (_isTutorial)
        //{
        //    _tutoEntry.SetActive(true);
        //}
        //else
        //{
        //    _stageEntry.SetActive(true);
        //}
        _currentRoom = _rooms.Find(r => r._isEntry);
        _currentRoom.gameObject.SetActive(true);
        //GameManager.Instance.OnPlayerChangeType.AddListener(ShowShadowMap);
    }


    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.U))
        {
            StageClear();
        }
    }

    public void StageClear()
    {
        if (_isClear)
            return;

        _isClear = true;

        
        _currentRoom._spawners.ForEach(rs => rs._door.DoorOpendAndClose(true));

    }

    public void StageStart()
    {

        _isClear = false;
        if (_currentRoom == null)
        {
            _currentRoom = _rooms.Find(r => r._isEntry);
        }
        _currentRoom._spawners.ForEach(rs => rs._door.DoorOpendAndClose(false));
    }

    public void ShowShadowMap()
    {
        bool isShadow = PlayerStates.Shadow == GameManager.Instance.currentPlayerSO.playerStates;
        _rooms.ForEach(r =>
        {
            r.SwitchMap(isShadow);
        });
        globalLight.color = isShadow ? _shadowLightColor : _normalLightColor;


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door _matchedDoor;
    public Room _matchedRoom;
    public int _openingDirection;

    public GameObject _closedDoorObj;
    public GameObject _normalDoorObj;
    public GameObject _shadowDoorObj;

    private float _moveCorrectionValue = 2f;

    public Collider2D _nextCamBound;

    void Start()
    {


    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Room") && _matchedDoor == null && other.gameObject != this.gameObject)
        {
            _matchedRoom = other.GetComponent<Room>();
            if(_matchedRoom != null)
            {
                RoomSpawner rsp = _matchedRoom._spawners.Find(rs => rs._openingDirection == _openingDirection);
                _nextCamBound = _matchedRoom._camBound;
                if (rsp != null)
                    if (rsp._door != null && _matchedDoor == null)
                    {
                        _matchedDoor = rsp._door;
                    }
                    else
                    {
                        print("ºñ¾úÀ½");
                    }
            }
        }


        
    }

    public void DoorOpendAndClose(bool isOpened = false)
    {
        _closedDoorObj.SetActive(!isOpened);
        _shadowDoorObj.SetActive(GameManager.Instance.currentPlayerSO.playerStates == PlayerStates.Shadow && StageManager.Instance._isClear);

    }

    public void SwitchDoorObj(bool isShadow)
    {
        _normalDoorObj.SetActive(!isShadow);
        _shadowDoorObj.SetActive(GameManager.Instance.currentPlayerSO.playerStates == PlayerStates.Shadow && StageManager.Instance._isClear);
        
    }

    public void MoveRoom()
    {
        print("Move!");
        StartCoroutine(EffectManager.Instance.FadeOut());
        StageManager.Instance._currentRoom = _matchedRoom;
        CameraManager.Instance._cinemachineCamConfiner.m_BoundingShape2D = _nextCamBound;
        Vector3 movePos = Vector3.zero;

        switch (_matchedDoor._openingDirection)
        {
            case 1:
                movePos = new Vector3(_matchedDoor.transform.position.x, _matchedDoor.transform.position.y + _moveCorrectionValue);
                break;
            case 2:
                movePos = new Vector3(_matchedDoor.transform.position.x, _matchedDoor.transform.position.y - _moveCorrectionValue);
                break;
            case 3:
                movePos = new Vector3(_matchedDoor.transform.position.x + _moveCorrectionValue, _matchedDoor.transform.position.y);
                break;
            case 4:
                movePos = new Vector3(_matchedDoor.transform.position.x - _moveCorrectionValue, _matchedDoor.transform.position.y);
                break;
            default:
                print("?");
                break;
        }
        print($"{movePos} {_matchedDoor.transform.position}");
        GameManager.Instance.player.position = movePos;
        StageManager.Instance.StageStart();
    }
}

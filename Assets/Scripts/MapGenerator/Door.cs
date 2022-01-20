using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door _matchedDoor;
    public int _openingDirection;
    private float _moveCorrectionValue = 5f;

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
            Room room = other.GetComponent<Room>();
            if(room != null)
            {
                RoomSpawner rsp = room._spawners.Find(rs => rs._openingDirection == _openingDirection);
                _nextCamBound = room._camBound;
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

   

    public void MoveRoom()
    {
        print("Move!");
        StartCoroutine(EffectManager.Instance.FadeOut());
        GameManager.Instance._cinemachineCamConfiner.m_BoundingShape2D = _nextCamBound;
        Vector3 movePos = Vector3.zero;

        switch (_matchedDoor._openingDirection)
        {
            case 1:
                movePos = new Vector3(_matchedDoor.transform.position.x, _matchedDoor.transform.position.y - _moveCorrectionValue);
                break;
            case 2:
                movePos = new Vector3(_matchedDoor.transform.position.x, _matchedDoor.transform.position.y + _moveCorrectionValue);
                break;
            case 3:
                movePos = new Vector3(_matchedDoor.transform.position.x - _moveCorrectionValue, _matchedDoor.transform.position.y);
                break;
            case 4:
                movePos = new Vector3(_matchedDoor.transform.position.x + _moveCorrectionValue, _matchedDoor.transform.position.y);
                break;
            default:
                print("?");
                break;
        }
        print($"{movePos} {_matchedDoor.transform.position}");
        GameManager.Instance.player.position = movePos;
    }
}

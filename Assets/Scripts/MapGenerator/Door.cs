using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Door _matchedDoor;
    public int _openingDirection;


    void Start()
    {


    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Room"))
        {
            print(_openingDirection);
            Room room = other.GetComponent<Room>();
            if(room != null)
            {
                RoomSpawner rsp = room._spawners.Find(rs => rs._openingDirection == _openingDirection);
                if (rsp != null)
                    if (rsp._door != null)
                    {
                        _matchedDoor = rsp._door;
                        print(_matchedDoor.name);
                    }
                    else
                    {
                        print("�����");
                    }
            }
        }

        if (other.CompareTag("Player"))
        {
            MoveRoom();
        }
    }

    public void MoveRoom()
    {
        print("Move!");
        //GameManager.Instance.player
    }
}

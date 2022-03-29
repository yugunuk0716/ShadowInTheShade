using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Room currentRoom;
   
    private void Start()
    {
        RoomManager.Instance.OnMoveRoomEvent.AddListener(() =>
        {
            currentRoom.doorList.ForEach(d => d.IsOpen = false);
        });

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DoorOpen();
        }
    }

    public void DoorOpen()
    {
        currentRoom.doorList.ForEach(d => d.IsOpen = true);
    }
}

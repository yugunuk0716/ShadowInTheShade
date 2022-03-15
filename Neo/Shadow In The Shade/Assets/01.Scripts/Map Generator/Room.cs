using System.Collections;
using System.Collections.Generic;
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


    public List<Room> childrenList = new List<Room>();
    public List<Door> doorList = new List<Door>();

    private bool isChecked = false;

    private Door leftDoor;
    private Door rightDoor;
    private Door topDoor;
    private Door bottomDoor;

    public Room(int width, int height)
    {
        Width = width;
        Height = height;
    }


    private void Start()
    {
        RoomManager.Instance.RegisterRoom(this);
        Door[] doors = GetComponentsInChildren<Door>();
        foreach (Door door in doors)
        {
            doorList.Add(door);
            switch (door.doorType)
            {
                case DoorType.Left:
                    leftDoor = door;
                    break;
                case DoorType.Right:
                    rightDoor = door;
                    break;
                case DoorType.Top:
                    topDoor = door;
                    break;
                case DoorType.Bottom:
                    bottomDoor = door;
                    break;
               
            }
        }
        
    }

    public void RemoveUnconnectedDoors()
    {
        if (isChecked)
            return;
        Debug.Log("removing doors");
        foreach (Door door in doorList)
        {
            switch (door.doorType)
            {
                case DoorType.Right:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DoorType.Left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DoorType.Top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DoorType.Bottom:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }
        isChecked = true;
    }


    public Room GetRight()
    {
        return RoomManager.Instance.FindRoom(X + 1, Y);
    }
    public Room GetLeft()
    {
        return RoomManager.Instance.FindRoom(X - 1, Y);
    }
    public Room GetTop()
    {
        return RoomManager.Instance.FindRoom(X, Y + 1);
    }
    public Room GetBottom()
    {
        return RoomManager.Instance.FindRoom(X, Y - 1);
    }


    public override void Reset()
    {
        //print("Reset");
        //throw new System.NotImplementedException();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }
#endif
}

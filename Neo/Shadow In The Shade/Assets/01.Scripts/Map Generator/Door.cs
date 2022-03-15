using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    Left,
    Right,
    Top,
    Bottom,
}

public class Door : MonoBehaviour
{
    public Vector2Int pairRoomPos;

    public DoorType doorType;
}

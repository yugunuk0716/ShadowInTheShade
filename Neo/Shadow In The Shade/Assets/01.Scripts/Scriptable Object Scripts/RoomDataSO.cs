using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Map/RoomSO")]
public class RoomDataSO : ScriptableObject
{
    [field:SerializeField]
    public string Name { get; set; }

    [field:SerializeField]
    public int X { get; set; }

    [field:SerializeField]
    public int Y { get; set; }
}

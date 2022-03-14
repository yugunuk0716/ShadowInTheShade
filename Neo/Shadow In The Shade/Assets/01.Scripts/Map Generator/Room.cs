using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : PoolableMono
{
    [field:SerializeField]
    public int Width { get; set; }
    [field:SerializeField]
    public int Height { get; set; }


    public List<Room> childrenList = new List<Room>();
    public List<int> spawnablePoint = new List<int>();
    //문을 생성할 수 있는 방향

    // 1 -> 위쪽으로 가는 문
    // 2 -> 아래쪽으로 가는 문
    // 3 -> 오른쪽으로 가는 문
    // 4 -> 왼쪽으로 가는 문

    public Room(int width, int height)
    {
        Width = width;
        Height = height;
    }



    

    public override void Reset()
    {
        print("Reset");
        //throw new System.NotImplementedException();
    }
}

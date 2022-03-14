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
    //���� ������ �� �ִ� ����

    // 1 -> �������� ���� ��
    // 2 -> �Ʒ������� ���� ��
    // 3 -> ���������� ���� ��
    // 4 -> �������� ���� ��

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

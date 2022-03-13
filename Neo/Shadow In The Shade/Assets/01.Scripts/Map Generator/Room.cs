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

    // Start is called before the first frame update
    void Start()
    {
        if (RoomManager.Instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }

        RoomManager.Instance.RegisterRoom(this);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(Width, Height));
    }

    public Vector3 GetCenterRoom()
    {
        return new Vector3(X * Width, Y * Height);
    }

    public override void Reset()
    {
        print("Reset");
        //throw new System.NotImplementedException();
    }
}

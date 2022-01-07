using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour 
{

	public GameObject[] _bottomRooms;
	public GameObject[] _topRooms;
	public GameObject[] _leftRooms;
	public GameObject[] _rightRooms;

	public GameObject _closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;

    void Update()
    {

        if (waitTime <= 0 && spawnedBoss == false)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}

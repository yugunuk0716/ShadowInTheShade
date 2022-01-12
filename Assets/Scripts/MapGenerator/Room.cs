using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour 
{

    public List<int> _movable = new List<int>();

    public List<RoomSpawner> _spawners = new List<RoomSpawner>();

    void Start()
    {

        RoomTemplates.Instance._rooms.Add(this);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            RoomSpawner rs = collision.GetComponent<RoomSpawner>();
            if (this.CompareTag("ClosedRoom"))
            {
                _spawners.Remove(rs);
                Destroy(rs._door.gameObject);
                return;
            }

            if (!_movable.Contains(rs._openingDirection) && !this.gameObject.CompareTag("ClosedRoom")) 
            {
                //print($"파괴 + {collision.gameObject.name}");
                if (rs._door != null)
                {
                    _spawners.Remove(rs);
                    Destroy(rs._door.gameObject);
                }
                else
                {
                    print($"{this.gameObject.name}에서 도어가 없잖아!");
                }
            }
        }
    }
}

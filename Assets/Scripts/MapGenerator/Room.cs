using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private RoomTemplates _templates;

    public List<int> _movable = new List<int>();

    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        _templates._rooms.Add(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            RoomSpawner rs = collision.GetComponent<RoomSpawner>();
            if (this.CompareTag("ClosedRoom"))
            {
                Destroy(rs._door.gameObject);
                return;
            }

            if (!_movable.Contains(rs._openingDirection) && !this.gameObject.CompareTag("ClosedRoom")) 
            {
                //print($"파괴 + {collision.gameObject.name}");
                if (rs._door != null)
                {
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

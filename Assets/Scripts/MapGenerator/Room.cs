using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    private RoomTemplates _templates;
    private SpriteRenderer sr;

    public List<int> _movable = new List<int>();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
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

            if (!_movable.Contains(rs._openingDirection)) 
            {
                //print($"파괴 + {collision.gameObject.name}");
                Destroy(rs._door.gameObject);
            }
        }
    }
}

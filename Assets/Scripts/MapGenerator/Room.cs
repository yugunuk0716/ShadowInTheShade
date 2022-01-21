using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, IResettable
{



    public List<int> _movable = new List<int>();

    public List<RoomSpawner> _spawners = new List<RoomSpawner>();

    public Collider2D _camBound;

    public event EventHandler Death;

    public bool _isEntry = false;

    public void Reset()
    {

    }

    private void Awake()
    {
        StageManager.Instance._rooms.Add(this);

        Death += (sender, e) =>
        {
            this.gameObject.SetActive(false);
        };
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnPoint"))
        {
            RoomSpawner rs = collision.GetComponent<RoomSpawner>();
            if (this.CompareTag("ClosedRoom"))
            {
                _spawners.Remove(rs);
                print($"문짝 부심 {rs}");
                Destroy(rs._door.gameObject);
                return;
            }

            if (!_movable.Contains(rs._openingDirection) && !this.gameObject.CompareTag("ClosedRoom")) 
            {
                //print($"파괴 + {collision.gameObject.name}");
                if (rs._door != null)
                {
                    _spawners.Remove(rs);
                    print($" {rs._openingDirection}와 때문에 {rs.gameObject.name}에서 {this.gameObject.name}의 Door가 부서짐");
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

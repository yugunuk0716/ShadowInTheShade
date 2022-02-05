using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    #region Room

    const int START_SIZE = 5;
    public List<Pool<Room>> _bottomRoomPool;
    public List<Pool<Room>> _topRoomPool;
    public List<Pool<Room>> _leftRoomPool;
    public List<Pool<Room>> _rightRoomPool;

    public GameObject[] _bottomRooms;
    public GameObject[] _topRooms;
    public GameObject[] _leftRooms;
    public GameObject[] _rightRooms;

    public Room _closedRoom;
    #endregion

    public GameObject _damagePopupPrefab;
    public Pool<DamagePopup> _damagePopupPool;

    public PoolManager()
    {
        
        _bottomRoomPool = new List<Pool<Room>>();
        _topRoomPool = new List<Pool<Room>>();
        _leftRoomPool = new List<Pool<Room>>();
        _rightRoomPool = new List<Pool<Room>>();

    }





    private void Awake()
    {
        CreateDamagePopup();
        CreateStage();

    }

 

    public void CreateDamagePopup()
    {
        _damagePopupPool = new Pool<DamagePopup>(new PrefabFactory<DamagePopup>(_damagePopupPrefab), START_SIZE);
    }   

    public void CreateStage() 
    {
        for (int i = 0; i < _bottomRooms.Length; i++)
        {
            Pool<Room> pool = new Pool<Room>(new PrefabFactory<Room>(_bottomRooms[i]), START_SIZE);

            _bottomRoomPool.Add(pool);
        }

        for (int i = 0; i < _topRooms.Length; i++)
        {
            Pool<Room> pool = new Pool<Room>(new PrefabFactory<Room>(_topRooms[i]), START_SIZE);

            _topRoomPool.Add(pool);
        }

        for (int i = 0; i < _leftRooms.Length; i++)
        {
            Pool<Room> pool = new Pool<Room>(new PrefabFactory<Room>(_leftRooms[i]), START_SIZE);

            _leftRoomPool.Add(pool);
        }

        for (int i = 0; i < _rightRooms.Length; i++)
        {
            Pool<Room> pool = new Pool<Room>(new PrefabFactory<Room>(_rightRooms[i]), START_SIZE);

            _rightRoomPool.Add(pool);
        }

    }

    public void SetStage(int index, Vector3 pos)
    {


        EventHandler handler = null;
        Room room = null;

        switch (index)
        {
            case 1:
                room = _bottomRoomPool[UnityEngine.Random.Range(0, _bottomRoomPool.Count)].Allocate();
                handler = (sender, e) =>
                {
                    _bottomRoomPool[index].Release(room);
                    room.Death -= handler;
                };
                break;
            case 2:
                room = _topRoomPool[UnityEngine.Random.Range(0, _topRoomPool.Count)].Allocate();
                handler = (sender, e) =>
                {
                    _topRoomPool[index].Release(room);
                    room.Death -= handler;
                };
                break;
            case 3:
                room = _leftRoomPool[UnityEngine.Random.Range(0, _leftRoomPool.Count)].Allocate();
                handler = (sender, e) =>
                {
                    _leftRoomPool[index].Release(room);
                    room.Death -= handler;
                };
                break;
            case 4:
                room = _rightRoomPool[UnityEngine.Random.Range(0, _rightRoomPool.Count)].Allocate();
                handler = (sender, e) =>
                {
                    _rightRoomPool[index].Release(room);
                    room.Death -= handler;
                };
                break;
            default:
                print($"이상한데?  {index}");
                break;
        }



      

        room.Death += handler;
        room.transform.position = pos;
        room.gameObject.SetActive(true);


    }
}

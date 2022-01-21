using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public List<Room> _rooms;
    public bool _isTutorial = false;
    //public GameObject _tutoEntry;
    //public GameObject _stageEntry;

    private void Awake()
    {
        _rooms = new List<Room>();
    }

    private void Start()
    {
        //if (_isTutorial)
        //{
        //    _tutoEntry.SetActive(true);
        //}
        //else
        //{
        //    _stageEntry.SetActive(true);
        //}
    }
}

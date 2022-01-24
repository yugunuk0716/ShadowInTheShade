using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    //Cinemachine Camera
    public GameObject _cinemachineCamObj;
    [HideInInspector]
    public CinemachineConfiner _cinemachineCamConfiner;
    [HideInInspector]
    public CinemachineVirtualCamera _cinemachineCam;

    // Start is called before the first frame update
    void Start()
    {
        _cinemachineCamConfiner = _cinemachineCamObj.GetComponent<CinemachineConfiner>();
        _cinemachineCam = _cinemachineCamObj.GetComponent<CinemachineVirtualCamera>();

        _cinemachineCamConfiner.m_BoundingShape2D = StageManager.Instance._rooms.Find((r) => r._isEntry)._camBound;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class StageManager : MonoSingleton<StageManager>
{
    public List<Room> _rooms;
    public bool _isTutorial = false;
    public Light2D globalLight;
    public Color _shadowLightColor;
    public Color _normalLightColor;
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShadowMap(true);
        }
        else if(Input.GetKeyDown(KeyCode.N))
        {
            ShadowMap();
        }
    }
    public void ShadowMap(bool isShadow = false)
    {
        _rooms.ForEach(r =>
        {
            r._normalMap.SetActive(!isShadow);
            r._shadowMap.SetActive(isShadow);
        });
        globalLight.color = isShadow ? _shadowLightColor : _normalLightColor;


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance { 
        get
        { 
            return _instance; 
        }
    }

    public static Transform Player
    {
        get
        {
            if (instance != null)
                return instance.player;
            else return null;
        }
    }
    public Transform player;

    private float timeScale = 1f;

    public static float TimeScale
    {
        get
        {
            if (instance != null)
                return instance.timeScale;
            return 0;
        }
        set
        {
            instance.timeScale = Mathf.Clamp(value, 0, 1);
        }
    }


    private void Awake()
    {
        if (_instance != null) 
        {
            Debug.LogError("다수의 GameManager가 실행중입니다");
        }
        _instance = this;

    }

}

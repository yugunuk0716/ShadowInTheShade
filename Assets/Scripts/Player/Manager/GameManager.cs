using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    

    public static Transform Player
    {
        get
        {
            if (Instance != null) 
                return Instance._player;
            else return null;
        }
    }
    public Transform _player;

    private float _timeScale = 1f;

    public static float TimeScale
    {
        get
        {
            if (Instance != null)
                return Instance._timeScale;
            return 0;
        }
        set
        {
            Instance._timeScale = Mathf.Clamp(value, 0, 1);
        }
    }


    

}

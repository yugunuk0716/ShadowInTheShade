using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class State_Chase : MonoBehaviour, IState
{
    public float speed = 3f;


    public void OnEnter()
    {
        transform.DOMove(GameManager.Instance.player.position, speed);
    }

    public void OnEnd()
    {
        
    }


    
}

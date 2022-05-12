using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Move_Chase : MonoBehaviour, IState
{
    public float speed = 3f;
    public bool canTrace = true;

    private Transform target;

    AIDestinationSetter destinationSetter;

    public void OnEnter()
    {


        if (target == null)
            target = GameManager.Instance.player;

        if (destinationSetter == null)
            destinationSetter = GetComponent<AIDestinationSetter>();


        if (canTrace)
        {
            destinationSetter.target = target;
        }
    }

    public void OnEnd()
    {
        destinationSetter.target = null;
    }


}

using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Chase_Astar : MonoBehaviour, IState
{
    public float speed = 3f;

    private Transform target;

    AIDestinationSetter destinationSetter;

    public void OnEnter()
    {


        if (target == null)
            target = GameManager.Instance.player;

        if(destinationSetter == null)
            destinationSetter = GetComponent<AIDestinationSetter>();



        destinationSetter.target = target;
    }

    public void OnEnd()
    {

        destinationSetter.target = null;

    }





}

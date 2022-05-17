using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Dice : MonoBehaviour , IState
{
    private readonly float pillarCool = 10f;
    private float lastTime = 0f;
    private Boss_Dice dice;

    public void OnEnter()
    {
        if(dice == null)
            dice = GetComponent<Boss_Dice>();

        if(Time.time - lastTime < pillarCool)
        {
            return;
        }



        lastTime = Time.time;

        SlimePillar sp = PoolManager.Instance.Pop("Slime Pillar") as SlimePillar;
        Vector3 curRoomPos = StageManager.Instance.currentRoom.transform.position;
        Vector3 pos = new Vector3(curRoomPos.x + Random.Range(1f, 15f), curRoomPos.y + Random.Range(1f, 10f));
        if (dice.slimePillars.Find(p => p.transform.position == pos) == null)
        {
            sp.transform.position = Random.Range(0, 2) == 0 ? pos : -pos;
        }
        dice.slimePillars.Add(sp);

    }


    

    public void OnEnd()
    {
        
    }

   
   
}

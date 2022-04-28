using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_Dice : MonoBehaviour , IState
{
    private readonly float pillarCool = 4f;
    private float lastTime = 0f;

    private List<SlimePillar> slimePillars = new List<SlimePillar>();   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            OnEnter();
        }
    }

    public void OnEnter()
    {
        if(Time.time - lastTime < pillarCool)
        {
            return;
        }

        lastTime = Time.time;

        SlimePillar sp = PoolManager.Instance.Pop("Slime Pillar") as SlimePillar;
        Vector3 curRoomPos = StageManager.Instance.currentRoom.transform.position;
        Vector3 pos = new Vector3(curRoomPos.x + Random.Range(1f, 15f), curRoomPos.y + Random.Range(1f, 10f));
        if (slimePillars.Find(p => p.transform.position == pos) == null)
        {
            sp.transform.position = Random.Range(0, 2) == 0 ? pos : -pos;
        }
        slimePillars.Add(sp);

    }


    

    public void OnEnd()
    {
        
    }

   
   
}

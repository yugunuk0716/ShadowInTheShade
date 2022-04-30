using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Dice : MonoBehaviour, IState
{

    Boss_Dice dice;

    public void OnEnter()
    {
        if(dice == null)
            dice = GetComponent<Boss_Dice>();
        dice.Anim.SetBool("isCrash", true);
    }

    public void OnEnd()
    {
        dice.Anim.SetBool("isCrash", false);
    }


}

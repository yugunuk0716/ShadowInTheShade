using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_Dice : MonoBehaviour, IState
{
    Boss_Dice dice;
    List<Boss_Dice> childDiceList = new List<Boss_Dice>();


    public void OnEnter()
    {
        if(dice == null)
            dice = GetComponent<Boss_Dice>();
        if (childDiceList.Count > 0)
            childDiceList.Clear();

        switch (dice.diceType)
        {
            case DiceType.Mk1:
                for (int i = 0; i < 2; i++)
                {
                    childDiceList.Add(PoolManager.Instance.Pop("Dice Mk2 Slime") as Boss_Dice);
                    childDiceList[i].transform.position = i == 0 ? transform.position + new Vector3(1, 0) : transform.position + new Vector3(-1, 0);
                }
                break;
            case DiceType.Mk2:
                for (int i = 0; i < 4; i++)
                {
                    childDiceList.Add(PoolManager.Instance.Pop("Dice Mk3 Slime") as Boss_Dice);
                    switch (i)
                    {
                        case 0:
                            childDiceList[i].transform.position = transform.position + new Vector3(1, 1);
                            break;
                        case 1:
                            childDiceList[i].transform.position = transform.position + new Vector3(-1, 1);
                            break;
                        case 2:
                            childDiceList[i].transform.position = transform.position + new Vector3(-1, -1);
                            break;
                        case 3:
                            childDiceList[i].transform.position = transform.position + new Vector3(1, -1);
                            break;
                    }
                }
                break;
            case DiceType.Mk3:
                DOTween.To(() => UIManager.Instance.bossHPBarCG.alpha, value => UIManager.Instance.bossHPBarCG.alpha = value, 0, 0.8f);
                
                break;
        }


    }

    public void OnEnd()
    {

    }


   

   
}

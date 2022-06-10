using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderScripts : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public ExpSlider exp;
    private PlayerSO pso;

    private void Start()
    {
        slider.value = 0f;
        FillSlider();
        
    }

    public void FillSlider()
    {
        fill.fillAmount = slider.value;
    }

    public void StatUIPlus(int input)
    {
        if (slider.value < 1f)
        {
            slider.value += 0.05f;
            FillSlider();
            pso = GameManager.Instance.playerSO;

            switch (input)
            {
                case 1:
                    pso.mainStats.STR += 1;
                    GameManager.Instance.InitMainStatPoint(1);
                    break;
                case 2:
                    pso.mainStats.DEX += 1;
                    GameManager.Instance.InitMainStatPoint(2);
                    break;
                case 3:
                    pso.mainStats.AGI += 1;
                    GameManager.Instance.InitMainStatPoint(3);
                    break;
                case 4:
                    pso.mainStats.SPL += 1;
                    GameManager.Instance.InitMainStatPoint(4);
                    break;
            }

            exp.statPoint--;
            exp.StatPointCheck();

            GameManager.Instance.onPlayerStatUp?.Invoke();
        }
    }

    public void ChangeClass(int index)
    {
        switch (index)
        {
            case 1:
                pso.playerJobState = PlayerJobState.Berserker;
                break;
            case 2:
                pso.playerJobState = PlayerJobState.Archer;
                break;
            case 3:
                pso.playerJobState = PlayerJobState.Greedy;
                break;
            case 4:
                pso.playerJobState = PlayerJobState.Devilish;
                break;
        }

        GameManager.Instance.ApplyClassChange();
    }
}

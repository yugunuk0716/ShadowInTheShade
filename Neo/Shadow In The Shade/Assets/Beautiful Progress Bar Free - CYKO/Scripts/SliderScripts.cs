using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderScripts : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    public ExpSlider exp;

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

            switch (input)
            {
                case 1:
                    GameManager.Instance.playerSO.mainStats.STR += 1;
                    GameManager.Instance.InitMainStatPoint(1);
                    break;
                case 2:
                    GameManager.Instance.playerSO.mainStats.DEX += 1;
                    GameManager.Instance.InitMainStatPoint(2);
                    break;
                case 3:
                    GameManager.Instance.playerSO.mainStats.AGI += 1;
                    GameManager.Instance.InitMainStatPoint(3);
                    break;
                case 4:
                    GameManager.Instance.playerSO.mainStats.SPL += 1;
                    GameManager.Instance.InitMainStatPoint(4);
                    break;
            }

            exp.statPoint--;
            exp.StatPointCheck();
        }

        
    }
}

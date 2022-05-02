using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderScripts : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    private void Start()
    {
        slider.value = 0f;
        FillSlider();
    }

    public void FillSlider()
    {
        fill.fillAmount = slider.value;
    }

    public void StatUIPlus()
    {
        slider.value += 0.05f;
        FillSlider();
    }
}

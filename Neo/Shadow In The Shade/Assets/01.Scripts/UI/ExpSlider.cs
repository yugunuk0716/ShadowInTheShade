using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpSlider : MonoBehaviour
{
    [SerializeField]
    private Slider expSlider;

    [SerializeField]
    private Text expLevel;

    [SerializeField]
    private List<float> needExpPointPerLever;

    public float statPoint = 0;

    public void ResetSlider()
    {
        expSlider.value = GameManager.Instance.playerSO.ectStats.EXP / needExpPointPerLever[(int)GameManager.Instance.playerSO.ectStats.LEV];
    }

    public void CheckExp()
    {
        if (GameManager.Instance.playerSO.ectStats.EXP >= needExpPointPerLever[(int)GameManager.Instance.playerSO.ectStats.LEV])
        {
            GameManager.Instance.playerSO.ectStats.LEV++;
            GameManager.Instance.playerSO.ectStats.EXP = 0;
            statPoint++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class DefualtButtons : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();
    public List<Vector3> originPos = new List<Vector3>();

    private void Start()
    {
        buttons = GetComponentsInChildren<Button>().ToList();

        for(int i =0; i < buttons.Count;i++)
        {
            originPos.Add(buttons[i].transform.localPosition);
            buttons[i].transform.localPosition += new Vector3(-600, 0, 0);
        }

        StartCoroutine(SettingButtons());
    }

    public IEnumerator SettingButtons()
    {
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].transform.DOLocalMove(originPos[i], .5f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(.1f);
        }
    }

    public void ReWind()
    {
        for(int i =0; i < buttons.Count;i++)
        {
            buttons[i].transform.DOLocalMove(originPos[i] += new Vector3(-600, 0, 0), .5f).SetEase(Ease.Linear);
        }
    }
}

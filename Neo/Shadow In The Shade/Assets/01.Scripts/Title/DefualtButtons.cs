using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using UnityEngine.SceneManagement;

public class DefualtButtons : MonoBehaviour
{
    private List<Button> buttons = new List<Button>();
    public List<Vector3> originPos = new List<Vector3>();

    public Text text;

    private void Start()
    {
        buttons = GetComponentsInChildren<Button>().ToList();

        for(int i =0; i < buttons.Count;i++)
        {
            originPos.Add(buttons[i].transform.localPosition);
            buttons[i].transform.localPosition += new Vector3(-600, 0, 0);
        }

        StartCoroutine(SettingButtons());

        text.DOFade(0f, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadInGameScene();
        }
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

    public void LoadInGameScene()
    {
        SceneManager.LoadScene(1);
    }
}

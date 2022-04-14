using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{

    public Button clearPanel;
    public Transform popupParent;
    public OptionPopup optionPopupPrefab;

    public bool _isPopuped = false;

    private CanvasGroup popupCanvasGroup;

    public Dictionary<string, Popup> popupDic = new Dictionary<string, Popup>();
    private Stack<Popup> popupStack = new Stack<Popup>();

    private void Start()
    {

        popupCanvasGroup = popupParent.GetComponent<CanvasGroup>();
        if (popupCanvasGroup == null)
        {
            popupCanvasGroup = popupParent.gameObject.AddComponent<CanvasGroup>();
        }
        //켄버스 그룹 초기화
        popupCanvasGroup.alpha = 0;
        popupCanvasGroup.interactable = false;
        popupCanvasGroup.blocksRaycasts = false;


       // popupDic.Add("option", Instantiate(optionPopupPrefab, popupParent));


        clearPanel.onClick.AddListener(() => 
        {
            EffectManager.Instance.StartFadeIn();
            SceneManager.LoadScene("Title");
        });
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPopuped)
            {
                ClosePopup();
            }
            else
            {
                OpenPopup("option");
            }
        }
       
    }

    public void OpenPopup(string name, object data = null, int closeCount = 1)
    {
        if (popupStack.Count == 0)
        {
            DOTween.To(() => popupCanvasGroup.alpha, value => popupCanvasGroup.alpha = value, 1, 0.8f).OnComplete(() =>
            {
                popupCanvasGroup.interactable = true;
                popupCanvasGroup.blocksRaycasts = true;
            });
            GameManager.Instance._timeScale = 0f;
            _isPopuped = true;
        }
        popupStack.Push(popupDic[name]);
        popupDic[name].Open(data, closeCount);
    }
    public void ClosePopup()
    {
        if(popupStack.Count > 0)
        {
            popupStack.Pop().Close();
            if (popupStack.Count == 0)
            {
                DOTween.To(() => popupCanvasGroup.alpha, value => popupCanvasGroup.alpha = value, 0, 0.8f).OnComplete(() =>
                {
                    popupCanvasGroup.interactable = false;
                    popupCanvasGroup.blocksRaycasts = false;
                    _isPopuped = false;
                GameManager.Instance._timeScale = 1f;
                });
            }
        }
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    public GameObject minimap;
    private bool getTab;

    [SerializeField]
    private Image playerHPBar;

    [SerializeField]
    private Image playerHPBar_White;

    [SerializeField]
    private Image playerEXPBar;

    public CanvasGroup bossHPBarCG;


    #region UI Popup
    public Transform popupParent;
    public OptionPopup optionPopupPrefab;

    public bool _isPopuped = false;

    private CanvasGroup popupCanvasGroup;

    public Dictionary<string, Popup> popupDic = new Dictionary<string, Popup>();
    private Stack<Popup> popupStack = new Stack<Popup>();
    #endregion

    #region Fade
    float a = 1;
    public Image fadeImage;
    #endregion

    public Text tooltipText;
    public Image tooltipIcon;
    public Image tooltipBG;

    private CanvasGroup tooltipCG;
    private Vector3 initPosition;

    private void Awake()
    {
        instance = this;

        tooltipCG = tooltipBG.GetComponent<CanvasGroup>();
    }

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


        popupDic.Add("option", Instantiate(optionPopupPrefab, popupParent));


    }

    private void Update()
    {
        getTab = Input.GetKey(KeyCode.Tab);

        DrawMiniMap(getTab);

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

        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    StartFadeOut();
        //}

        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    StartFadeOut();
        //}

    }


    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        while (true)
        {
            a += 0.01f;
           
            fadeImage.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
            if (a >= 1)
                break;
        }


        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        a = 1f;
        while (true)
        {
            a -= 0.01f;
            fadeImage.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.01f);
            if (a <= 0)
                break;
        }

    }

    private void DrawMiniMap(bool on)
    {
        minimap.SetActive(on);
    }

    public void SetBar(float value, bool isEXP = false)
    {
        if (isEXP)
        {
            playerEXPBar.fillAmount = value;
            return;
        }

        playerHPBar.fillAmount = value;
        playerHPBar_White.DOFillAmount(playerHPBar.fillAmount, .3f).SetEase(Ease.Linear).SetDelay(.5f);
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
            GameManager.Instance.timeScale = 0f;
            _isPopuped = true;
        }
        popupStack.Push(popupDic[name]);
        popupDic[name].Open(data, closeCount);
    }
    public void ClosePopup()
    {
        if (popupStack.Count > 0)
        {
            popupStack.Pop().Close();
            if (popupStack.Count == 0)
            {
                DOTween.To(() => popupCanvasGroup.alpha, value => popupCanvasGroup.alpha = value, 0, 0.8f).OnComplete(() =>
                {
                    popupCanvasGroup.interactable = false;
                    popupCanvasGroup.blocksRaycasts = false;
                    _isPopuped = false;
                    GameManager.Instance.timeScale = 1f;
                });
            }
        }
    }

    public void ShowToolTip(string text, Sprite icon)
    {
        tooltipText.text = text;
        tooltipIcon.sprite = icon;

        CanvasGroup cg = tooltipCG;
        DOTween.To(() => cg.alpha, value => cg.alpha = value, 1, 0.8f);
    }


    public void CloseTooltip()
    {
        DOTween.Clear(); //모든 트윈을 종료시키고 
        
        CanvasGroup cg = tooltipCG;
        DOTween.To(() => cg.alpha, value => cg.alpha = value, 0, 0.8f);
    }


}

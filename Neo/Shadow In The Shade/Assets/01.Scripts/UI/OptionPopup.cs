using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopup : Popup
{

    #region Resolution
    private const string RESOLUTION_KEY = "resolution";
    private const string SCREENMODE_KEY = "screen";

    public Text resolutionText;
    public Text screenModeText;
    public Button previousButton;
    public Button nextButton;
    public Button previousScreenModeButton;
    public Button nextScreenModeScreenButton;
    public Button applyChangeButton;

    private bool isFullScreen;
    private int screenModeNumber;
    private Resolution[] resolutions;

    private int currentResolution = 0;

    #endregion




    protected override void Awake()
    {
        base.Awake();


    }

    private void Start()
    {
        #region ResolutionSetting

        if (PlayerPrefs.GetInt(SCREENMODE_KEY, 0) == 0)
        {
            isFullScreen = false;
        }
        else
        {
            isFullScreen = true;
        }
        screenModeText.text = isFullScreen ? "전체화면" : "창 모드";


        applyChangeButton.onClick.AddListener(() =>
        {
            ApplyChanges();
            UIManager.Instance.ClosePopup();
        });

        resolutions = Screen.resolutions;
        currentResolution = PlayerPrefs.GetInt(RESOLUTION_KEY, 0);


        previousButton.onClick.AddListener(SetPreviousResolution);
        nextButton.onClick.AddListener(SetNextResolution);

        previousScreenModeButton.onClick.AddListener(SetScreenMode);
        nextScreenModeScreenButton.onClick.AddListener(SetScreenMode);

        //print($"{Screen.currentResolution.width} x {Screen.currentResolution.height}");

        if (currentResolution != 0)
        {
            SetAndApplyResolution(currentResolution);
        }
        else
        {

            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        SetResolutionText(resolutions[currentResolution]);
        #endregion


    }


    #region ResolutionMethod

    private void SetScreenMode()
    {
        isFullScreen = !isFullScreen;
        screenModeText.text = isFullScreen ? "전체화면" : "창 모드";
        SetAndApplyResolution(currentResolution);
    }

    private void SetResolutionText(Resolution resolution)
    {
        //print(resolutions[currentResolution]);
        resolutionText.text = $"{resolution.width} x {resolution.height}";
    }

    public void SetNextResolution()
    {
        currentResolution = GetNextWrappedIndex(resolutions, currentResolution);
        SetResolutionText(resolutions[currentResolution]);
    }
    public void SetPreviousResolution()
    {
        currentResolution = GetPreviousWrappedIndex(resolutions, currentResolution);
        SetResolutionText(resolutions[currentResolution]);
    }
    private int GetNextWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1)
            return 0;
        return (currentIndex + 1) % collection.Count;
    }

    private int GetPreviousWrappedIndex<T>(IList<T> collection, int currentIndex)
    {
        if (collection.Count < 1)
            return 0;
        if ((currentIndex - 1) < 0)
            return collection.Count - 1;
        return (currentIndex - 1) % collection.Count;

    }

    private void SetAndApplyResolution(int newResolutionIndex)
    {
        currentResolution = newResolutionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyResolution(resolutions[currentResolution]);
    }

    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);

        Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
        PlayerPrefs.SetInt(RESOLUTION_KEY, currentResolution);

        screenModeNumber = isFullScreen ? 1 : 0;

        PlayerPrefs.SetInt(SCREENMODE_KEY, screenModeNumber);
    }

    public void ApplyChanges()
    {
        SetAndApplyResolution(currentResolution);
    }
    #endregion

}
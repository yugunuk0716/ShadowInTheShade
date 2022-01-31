using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionPopUp : PopUp
{

    #region Resolution
    private const string RESOLUTION_KEY = "resolution";
    private const string SCREENMODE_KEY = "screen";

    public Text _resolutionText;
    public Text _screenModeText;
    public Button _previousButton;
    public Button _nextButton;
    public Button _previousScreenModeButton;
    public Button _nextScreenModeScreenButton;
    public Button _applyChangeButton;

    private bool _isFullScreen;
    private int _screenModeNumber;
    private Resolution[] _resolutions;

    private int _currentResolution = 0;

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
            _isFullScreen = false;
        }
        else
        {
            _isFullScreen = true;
        }
        _screenModeText.text = _isFullScreen ? "전체화면" : "창 모드";


        _applyChangeButton.onClick.AddListener(() => 
        {
            ApplyChanges();
            UIManager.Instance.ClosePopup();
        });

        _resolutions = Screen.resolutions;
        _currentResolution = PlayerPrefs.GetInt(RESOLUTION_KEY, 0);


        _previousButton.onClick.AddListener(SetPreviousResolution);
        _nextButton.onClick.AddListener(SetNextResolution);

        _previousScreenModeButton.onClick.AddListener(SetScreenMode);
        _nextScreenModeScreenButton.onClick.AddListener(SetScreenMode);

        print($"{Screen.currentResolution.width} x {Screen.currentResolution.height}");

        if (_currentResolution != 0)
        {
            SetAndApplyResolution(_currentResolution);
        }
        else
        {
           
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        SetResolutionText(_resolutions[_currentResolution]);
        #endregion


    }




    #region ResolutionMethod

    private void SetScreenMode()
    {
        _isFullScreen = !_isFullScreen;
        _screenModeText.text = _isFullScreen ? "전체화면" : "창 모드";
        SetAndApplyResolution(_currentResolution);
    }

    private void SetResolutionText(Resolution resolution)
    {
        _resolutionText.text = $"{resolution.width} x {resolution.height}";
    }

    public void SetNextResolution()
    {
        _currentResolution = GetNextWrappedIndex(_resolutions, _currentResolution);
        SetResolutionText(_resolutions[_currentResolution]);
    }
    public void SetPreviousResolution()
    {
        _currentResolution = GetPreviousWrappedIndex(_resolutions, _currentResolution);
        SetResolutionText(_resolutions[_currentResolution]);
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
        _currentResolution = newResolutionIndex;
        ApplyCurrentResolution();
    }

    private void ApplyCurrentResolution()
    {
        ApplyResolution(_resolutions[_currentResolution]);
    }

    private void ApplyResolution(Resolution resolution)
    {
        SetResolutionText(resolution);

        Screen.SetResolution(resolution.width, resolution.height, _isFullScreen);
        PlayerPrefs.SetInt(RESOLUTION_KEY, _currentResolution);

        _screenModeNumber = _isFullScreen ? 1 : 0;

        PlayerPrefs.SetInt(SCREENMODE_KEY, _screenModeNumber);
    }

    public void ApplyChanges()
    {
        SetAndApplyResolution(_currentResolution);
    }
    #endregion

}

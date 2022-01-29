using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowManager : MonoBehaviour
{

    private const string RESOLUTION_KEY = "resolution";

    public Text _resolutionText;
    public Text _windowedText;
    public Button _previousButton;
    public Button _nextButton;
    public Button _windowedButton;

    private bool _isWindowed;
    private Resolution[] _resolutions;

    private int _currentResolution = 0;

    private void Start()
    {
        //Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        _windowedText = _windowedButton.GetComponentInChildren<Text>();
        _windowedButton.onClick.AddListener(() => 
        {
            _isWindowed = !_isWindowed; 
            _windowedText.text = $"{_isWindowed}";
            SetAndApplyResolution(_currentResolution);
        });

        _resolutions = Screen.resolutions;

        _currentResolution = PlayerPrefs.GetInt(RESOLUTION_KEY, 0);

        _previousButton.onClick.AddListener(SetPreviousResolution);
        _nextButton.onClick.AddListener(SetNextResolution);

        SetResolutionText(_resolutions[_currentResolution]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetResolutionText(_resolutions[_currentResolution]);
        }
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

        Screen.SetResolution(resolution.width, resolution.height, _isWindowed);
        PlayerPrefs.SetInt(RESOLUTION_KEY, _currentResolution);
    }

    public void ApplyChanges()
    {
        SetAndApplyResolution(_currentResolution);
    }

}

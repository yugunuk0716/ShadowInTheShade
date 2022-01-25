using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public Button _gameStartButton;
    public Button _logBookButton;
    public Button _optionButton;
    public Button _quitButton;

    public Button _panel;

    private void Start()
    {
        _gameStartButton.onClick.AddListener(() => SceneManager.LoadScene("InGame"));
        _quitButton.onClick.AddListener(() => Application.Quit());
        _logBookButton.onClick.AddListener(ShowPanel);
        _optionButton.onClick.AddListener(ShowPanel);
        _panel.onClick.AddListener(ClosePanel);

    }


    void ShowPanel()
    {
        _panel.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        _panel.gameObject.SetActive(false);

    }    
}

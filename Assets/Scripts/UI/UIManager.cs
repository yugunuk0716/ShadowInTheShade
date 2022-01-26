using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public Button clearPanel;

    private void Start()
    {
        clearPanel.onClick.AddListener(() => 
        {
            EffectManager.Instance.StartFadeIn();
            SceneManager.LoadScene("Title");
        });
    }
}

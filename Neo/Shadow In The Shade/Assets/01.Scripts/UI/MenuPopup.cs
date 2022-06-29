using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPopup : Popup
{

    public Button quitButton;
    public Button cancelButton;
    public Button optionButton;


    protected override void Awake()
    {
        base.Awake();

        quitButton.onClick.AddListener(() => 
        {
            Quit();
        });

        cancelButton.onClick.AddListener(() =>
        {
           Close();
        });

        optionButton.onClick.AddListener(() =>
        {
            Close();
            UIManager.Instance.OpenPopup("option");
        });
    }


    public void Quit()
    {
        Application.Quit();
    }
}

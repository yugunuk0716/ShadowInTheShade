using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Image playerEXPBar;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        getTab = Input.GetKey(KeyCode.Tab);

        DrawMiniMap(getTab);
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
    }
    



}

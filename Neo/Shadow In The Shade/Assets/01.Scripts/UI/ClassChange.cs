using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassChange : MonoBehaviour
{
    public List<Button> classbuttons = new List<Button>();

    private PlayerSO playerso;

    public void Start()
    {
        for(int i = 0; i< classbuttons.Count;i++)
        {
            classbuttons[i].gameObject.SetActive(false);
        }

        GameManager.Instance.onPlayerStatUp.AddListener(ShowClassChangeImage);
        playerso = GameManager.Instance.playerSO;
    }

    public void ShowClassChangeImage()
    {
        if (playerso.mainStats.STR > 14)
        {
            classbuttons[0].gameObject.SetActive(true);
        }

        if (playerso.mainStats.DEX > 14)
        {
            classbuttons[1].gameObject.SetActive(true);
        }

        if (playerso.mainStats.AGI > 14)
        {
            classbuttons[2].gameObject.SetActive(true);
        }

        if (playerso.mainStats.SPL > 14)
        {
            classbuttons[3].gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudUI : MonoBehaviour
{
    public Image _playerHpBar;
    public Image _playerProfile;


    public void UpdateUI(float currentHP, float maxHP)
    {
        _playerHpBar.fillAmount = currentHP / maxHP;
    }
}

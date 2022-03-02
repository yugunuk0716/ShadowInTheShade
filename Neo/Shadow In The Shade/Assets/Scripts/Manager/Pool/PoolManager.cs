using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    const int START_SIZE = 5;
    public GameObject _damagePopupPrefab;
    public Pool<DamagePopup> _damagePopupPool;

    private void Awake()
    {
        CreateDamagePopup();
    }

    public void CreateDamagePopup()
    {
        _damagePopupPool = new Pool<DamagePopup>(new PrefabFactory<DamagePopup>(_damagePopupPrefab), START_SIZE);
    }
}

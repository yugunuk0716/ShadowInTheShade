using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour, IResettable
{
    public int stageIndex;

    public event EventHandler Death;

    public void Reset()
    {

    }


    private void Start()
    {
        Death += (sender, e) =>
        {
            this.gameObject.SetActive(false);
        };
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tlqkfalsgud : MonoBehaviour
{
    public GameObject popUp;
    public GameObject box;
    public GameObject item;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject get;
    public GameObject noget;
    public bool isopen;

    public void Start()
    {
       
        resett();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            isopen = !isopen;
            resett();
            Set();
        }
    }

    public void Set()
    {
        if (!isopen)
        {
            popUp.SetActive(true);
            box.SetActive(true);
        }
        else
        {
            resett();
        }
    }

    public void clickbox()
    {
        box.SetActive(false);
        item.SetActive(true);
        get.SetActive(true);
        noget.SetActive(true);
    }

    public void getitem()
    {
        get.SetActive(false);
        noget.SetActive(false);
        item.SetActive(false);
        door1.SetActive(true);
        door2.SetActive(true);
        door3.SetActive(true);
    }

    public void clickDoor()
    {
        resett();
    }


    public void resett()
    {
        isopen = false;
        popUp.SetActive(false);
        box.SetActive(false);
        item.SetActive(false);
        door1.SetActive(false);
        door2.SetActive(false);
        door3.SetActive(false);
        get.SetActive(false);
        noget.SetActive(false);
    }
}

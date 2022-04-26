using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterection : MonoBehaviour
{
    private PlayerInput playerInput;
    private Interactable thing = null;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        if (playerInput.isUse && thing != null)
        {
            print("응애");
            thing.Use(gameObject);
        }
        else
        {
            print(thing != null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable i = collision.gameObject.GetComponent<Interactable>();
        if (i != null)
        {
            print($"오브젝트 들어옴 {i.gameObject.name}");
            thing = i;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable i = collision.gameObject.GetComponent<Interactable>();
        if (i != null)
        {
            print($"오브젝트 나감 {i.gameObject.name}");
            thing = null;
        }
    }

}

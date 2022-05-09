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
            thing.Use(gameObject);
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interactable i = collision.gameObject.GetComponent<Interactable>();
        if (i != null)
        {
            thing = i;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interactable i = collision.gameObject.GetComponent<Interactable>();
        if (i != null)
        {
            thing = null;
        }
    }

}

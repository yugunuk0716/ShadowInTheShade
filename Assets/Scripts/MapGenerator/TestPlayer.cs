using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door")) 
        {
            Door door = collision.gameObject.GetComponent<Door>();

            if(door != null)
            {
                door.MoveRoom();
            }
        }
    }
}

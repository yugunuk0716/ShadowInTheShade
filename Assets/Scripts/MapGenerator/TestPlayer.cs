using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") && StageManager.Instance._isClear) 
        {
            Door door = collision.gameObject.GetComponent<Door>();

            if(door != null)
            {
                door.MoveRoom();
            }
        }
        else if (collision.CompareTag("ClearTrigger"))
        {
            StageManager.Instance.StageClear();
        }
    }
}

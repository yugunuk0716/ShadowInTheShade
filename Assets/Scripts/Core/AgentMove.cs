using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMove :  MonoBehaviour , IMoveable
{

    public Rigidbody2D rid;

    public float speed;

    public void Awake()
    {
        rid = GetComponent<Rigidbody2D>();
    }
    public virtual void OnMove(Vector2 dir, float speed)
    {
        rid.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }
}

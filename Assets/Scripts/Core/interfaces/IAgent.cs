using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgent 
{
    int Health { get; }

    UnityEvent OnDie { get; set; }
    UnityEvent OnHit { get; set; }
}

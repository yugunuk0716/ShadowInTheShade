using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgent
{

    UnityEvent OnDie { get; set; }
    UnityEvent OnHit { get; set; }
}

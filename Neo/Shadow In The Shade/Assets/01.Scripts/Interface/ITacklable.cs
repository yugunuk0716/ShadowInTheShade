using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITacklable
{
    public bool CanAttack { get; }
    public void SetTackle(bool on);
}

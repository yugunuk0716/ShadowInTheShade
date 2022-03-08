using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/PoolingList")]
public class PoolingListSO : ScriptableObject
{
    public List<PoolableMono> list;
}

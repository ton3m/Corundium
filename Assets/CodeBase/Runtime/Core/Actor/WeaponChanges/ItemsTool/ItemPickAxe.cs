using System;
using UnityEngine;

public class ItemPickAxe: MonoBehaviour, IItemsTool
{
    public Type Type { get; }
    [field: SerializeField] public Transform PointForTip { get; private set; }
}

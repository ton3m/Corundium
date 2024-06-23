using System;
using UnityEngine;

public class ItemSword: MonoBehaviour, IItemsTool
{ 
    public Type Type { get; }
    [field: SerializeField] public Transform PointForTip { get; private set; }
}

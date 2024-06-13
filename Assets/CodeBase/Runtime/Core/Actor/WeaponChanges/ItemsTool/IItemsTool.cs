using System;
using UnityEngine;

public interface IItemsTool
{
        Type Type { get; }
        Transform PointForTip { get; }
}

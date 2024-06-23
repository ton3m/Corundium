using System;
using System.Numerics;
using CodeBase.Runtime.Core.Inventory;
using UnityEngine;

public interface IResource
{
    Item Item { get; }
    Transform PointForTip { get; }
    
    
}

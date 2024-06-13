using System;
using System.Numerics;
using UnityEngine;

public interface IResource
{
    Type Type { get; }
    Transform PointForTip { get; }
    
    
}

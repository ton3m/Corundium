using System;
using CodeBase.Runtime.Core.Inventory;
using Mirror;
using UnityEngine;

public class DropStone : NetworkBehaviour, IResource
{
    [field: SerializeField] public Item Item { get; private set; }
    [field: SerializeField] public Transform PointForTip { get; private set; }
}

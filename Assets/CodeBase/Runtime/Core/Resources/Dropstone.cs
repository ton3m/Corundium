using System;
using Mirror;
using UnityEngine;

public class DropStone : NetworkBehaviour, IResource
{
    [field: SerializeField] public Type Type{ get; private set; }
}

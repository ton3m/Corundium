using Mirror;
using UnityEngine;

namespace UnityEditor.ShaderGraph.Drawing
{
    public class Stone : NetworkBehaviour, IResource
    {
        [field: SerializeField] public int Quantity { get; private set; }
    }
}
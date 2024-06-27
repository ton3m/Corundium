using UnityEngine;

namespace CodeBase.Runtime.Core.Transport
{
    public interface ITransport
    {
        void Interact(Transform sender);
    }
}
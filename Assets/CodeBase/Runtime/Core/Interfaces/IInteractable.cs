using UnityEngine;

public interface IInteractable 
{
    public Transform PointForTip { get; }
    void Interact();
}

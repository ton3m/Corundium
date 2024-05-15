using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : NetworkBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private float _maxDistanceRaycast = 1.5f;
    private RaycastHit _hitInfo;
    private int _countStone; // временно, пока нет инвентаря

    private void Start()
    {
        if (!isLocalPlayer)
            return;
        
        _inputHandler.InteractPerformed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        _inputHandler.InteractPerformed -= OnInteractPerformed;
    }

    private void OnInteractPerformed()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hitInfo, _maxDistanceRaycast))
        {
            if (_hitInfo.transform.TryGetComponent(out IResource resource))
            {
                // Update Inventory
                _countStone += resource.Quantity; // временно
                Destroy(_hitInfo.transform.gameObject);
            }

            if (_hitInfo.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
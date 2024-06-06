using System.Runtime.InteropServices;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInteract : NetworkBehaviour
{
    [SerializeField] private float _maxDistanceRaycast = 1.5f;
    private IInputHandler _inputHandler;
    private RaycastHit _hitInfo;
    private LayerMask _layer;
    

    [Inject]
    public void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

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
                
                Destroy(_hitInfo.transform.gameObject);
            }

            if (_hitInfo.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
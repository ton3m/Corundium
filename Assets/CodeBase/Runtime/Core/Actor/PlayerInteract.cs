using System.Runtime.InteropServices;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInteract : NetworkBehaviour
{
    [SerializeField] private float _maxDistanceRaycast = 3f;
    private IInputHandler _inputHandler;
    private RaycastHit _hitInfo;
    private LayerMask _layer;
    
    [SerializeField] private PlayerWeaponController _playerWeaponController;
    
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

            if (_hitInfo.transform.TryGetComponent(out IItemsTool itemsTool))
            {
                bool check = _playerWeaponController.SetToolAvailable(itemsTool.GetType());
                if (check)
                {
                    Destroy(_hitInfo.transform.gameObject);
                }
            }

            if (_hitInfo.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
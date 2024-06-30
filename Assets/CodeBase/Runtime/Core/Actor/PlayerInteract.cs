using System.Runtime.InteropServices;
using CodeBase.Runtime.Core.Transport;
using Mirror;
using UnityEngine;
using System;
using System.Reflection;
using CodeBase.Runtime.Core.Inventory;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInteract : NetworkBehaviour
{
    [SerializeField] private float _maxDistanceRaycast = 3f;
    private IInputHandler _inputHandler;
    private RaycastHit _hitInfo;
    private LayerMask _layer;
    [SerializeField] private PlayerWeaponController _playerWeaponController;
    
    private TipsShower _tipsShower;
    [SerializeField]private Camera mainCamera;
    private IInventory _inventory;
    
    [Inject]
    public void Construct(IInputHandler inputHandler, TipsShower tipsShower, IInventory inventory)
    {
        _inventory = inventory;
        _inputHandler = inputHandler;
        _tipsShower = tipsShower;
    }

    private void Start()
    {
        if (!isLocalPlayer)
            return;
        
        Debug.Log("tips"+_tipsShower);
        _inputHandler.InteractPerformed += OnInteractPerformed;
    }

    private void Update()
    {
        
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hitInfo, _maxDistanceRaycast))
        {
            if (_hitInfo.transform.TryGetComponent(out IResource resource))
            {
                _tipsShower.ShowTip(resource.PointForTip, mainCamera.transform);
            }
            else if (_hitInfo.transform.TryGetComponent(out IItemsTool itemsTool))
            {
                _tipsShower.ShowTip(itemsTool.PointForTip, mainCamera.transform);
            }
            else if (_hitInfo.transform.TryGetComponent(out IInteractable interact))
            {
                _tipsShower.ShowTip(interact.PointForTip, mainCamera.transform);
            }
            else
            {
                _tipsShower.CloseTip();
            }
        }
        else
        {
            _tipsShower.CloseTip();
        }
    }
    private void OnDisable()
    {
        _inputHandler.InteractPerformed -= OnInteractPerformed;
    }

    private void OnInteractPerformed()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out _hitInfo, _maxDistanceRaycast))
        {
            if (_hitInfo.transform.TryGetComponent(out IResource resource))
            {
                // Update Inventory
                _inventory.TryAdd(resource.Item);
                
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
            
            if (_hitInfo.transform.TryGetComponent(out ITransport transport))
            {
                transport.Interact(transform);
            }
        }
    }
}